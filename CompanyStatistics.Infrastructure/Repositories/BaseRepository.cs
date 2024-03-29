﻿using CompanyStatistics.Domain.Abstraction.Repositories;
using CompanyStatistics.Domain.Exceptions;
using CompanyStatistics.Domain.Extensions;
using CompanyStatistics.Domain.Pagination;
using CompanyStatistics.Infrastructure.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Reflection;
using System.Text;

namespace CompanyStatistics.Infrastructure.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository
        where TEntity : BaseEntity
    {
        protected readonly string _connectionString;
        protected readonly string _dbConnectionString;
        protected string TableName { get; set; }

        public BaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CompanyStatisticsConnection");
            _dbConnectionString = SetDbConnectionString(_connectionString);
        }

        private string SetDbConnectionString(string connectionString)
        {
            return connectionString.Replace("master", "CompanyStatisticsDB");
        }

        private async Task<bool> CheckIfDbExistsAsync()
        {
            var dataTable = new DataTable();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("DECLARE @MyTableVar table([test] [varchar](250)); " +
                    "IF DB_ID('CompanyStatisticsDB') IS NOT NULL INSERT INTO @MyTableVar (test) values('db exists') " +
                    "ELSE INSERT INTO @MyTableVar (test) values('no') " +
                    "SELECT * FROM @MyTableVar", connection);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    dataTable.Load(reader);
                }
            }

            if (dataTable.Rows[0]["test"].ToString() == "db exists")
                return true;

            return false;
        }

        protected virtual async Task CreateDbIfNotExist()
        {
            if (await CheckIfDbExistsAsync())
            {
                return;
            }
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("CreateCompanyStatisticsDb", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                await cmd.ExecuteNonQueryAsync();
            }
        }
        protected string GenerateColumnsForInsert(PropertyInfo[] properties)
        {
            return string.Join(", ", properties.Select(x => x.Name));
        }

        protected StringBuilder GenerateValuesForInsert<TInput>(TInput entity, PropertyInfo[] properties)
        {
            StringBuilder values = new StringBuilder();

            for (int i = 0; i < properties.Count(); i++)
            {
                if (properties[i].PropertyType == typeof(string) || properties[i].PropertyType == typeof(Guid))
                {
                    values.Append($"'{properties[i].GetValue(entity).ToString().Replace("'", "''").Replace("\"", "")}'");
                }
                else
                {
                    values.Append($"{properties[i].GetValue(entity)}");
                }

                if (i + 1 < properties.Count())
                {
                    values.Append(',');
                }
            }

            return values;
        }

        protected virtual TOutput DataRowToEntity<TOutput>(DataRow dataRow)
            where TOutput : new()
        {
            return new TOutput();
        }

        protected virtual List<TOutput> DataTableToCollection<TOutput>(DataTable table)
            where TOutput : new()
        {
            if (table == null)
            {
                return null;
            }

            return table
                .AsEnumerable()
                .Select(x => DataRowToEntity<TOutput>(x))
                .ToList();
        }

        public virtual async Task<TOutput> InsertAsync<TInput, TOutput>(TInput entity)
            where TOutput : new()
        {
            await CreateDbIfNotExist();

            var properties = typeof(TInput).GetProperties().Where(x => x.CanRead).ToArray();

            var columnNames = GenerateColumnsForInsert(properties);
            var values = GenerateValuesForInsert(entity, properties);

            var dataTable = new DataTable();
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(
                    "USE CompanyStatisticsDB; DECLARE @MyTableVar table([testID] [varchar](250)); " +
                    $"INSERT INTO {TableName} ({columnNames}) " +
                    "OUTPUT INSERTED.Id INTO @MyTableVar " +
                    $"VALUES ({values}) ;" +
                    $"SELECT * FROM {TableName} WHERE Id = CAST((SELECT TOP 1 testID from @MyTableVar) AS nvarchar(250))", connection);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    dataTable.Load(reader);
                }
            }
            return DataRowToEntity<TOutput>(dataTable.Rows[0]);
        }

        public virtual async Task<TOutput> GetByIdAsync<TOutput>(string id)
            where TOutput : new()
        {
            await CreateDbIfNotExist();

            var dataTable = new DataTable();
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand($@"SELECT * FROM {TableName} WHERE Id = @Id AND IsDeleted = 0", connection);
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    dataTable.Load(reader);
                }
            }

            if (dataTable.Rows.Count == 0)
            {
                throw new NotFoundException();
            }

            return DataRowToEntity<TOutput>(dataTable.Rows[0]);
        }

        public virtual async Task<PaginatedResult<TOutput>> GetPageAsync<TOutput>(int pageNumber, int pageSize)
            where TOutput : new()
        {
            var dataTable = new DataTable();
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand($"USE CompanyStatisticsDB; SELECT TOP ({pageSize + pageSize * pageNumber}) * FROM {TableName} WHERE IsDeleted = 0", connection);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    dataTable.Load(reader);
                }
            }

            return dataTable
                .AsEnumerable()
                .Select(x => DataRowToEntity<TOutput>(x))
                .ToList()
                .Paginate(pageNumber, pageSize);
        }

        public async Task<TOutput> UpdateAsync<TInput, TOutput>(string id, TInput entity)
            where TOutput : new()
        {
            await CreateDbIfNotExist();

            var properties = typeof(TInput).GetProperties().Where(x => x.CanRead).ToArray();
            StringBuilder valuesToProps = new StringBuilder();

            for (int i = 0; i < properties.Count(); i++)
            {
                if (properties[i].PropertyType == typeof(string))
                {
                    valuesToProps.Append(properties[i].Name + "= " + $"'{properties[i].GetValue(entity)}'");
                }
                else
                {
                    valuesToProps.Append(properties[i].Name + "= " + properties[i].GetValue(entity));
                }

                if (i + 1 < properties.Count())
                {
                    valuesToProps.Append(',');
                }
            }

            var dataTable = new DataTable();
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand($"DECLARE @MyTableVar table([testID] [varchar](250)); " +
                    $"USE CompanyStatisticsDB; UPDATE {TableName} SET {valuesToProps} OUTPUT INSERTED.Id INTO @MyTableVar " +
                    $"WHERE Id = '{id}' AND IsDeleted = 0 " +
                    $"SELECT * FROM {TableName} WHERE Id = (SELECT TOP 1 testID from @MyTableVar)", connection);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    dataTable.Load(reader);
                }
            }
            return DataRowToEntity<TOutput>(dataTable.Rows[0]);
        }

        public virtual async Task DeleteAsync(string id)
        {
            await CreateDbIfNotExist();

            var dataTable = new DataTable();
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand($"UPDATE {TableName} SET IsDeleted = 1 WHERE Id = @Id; SELECT @@ROWCOUNT AS RowCounts", connection);
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    dataTable.Load(reader);
                }
            }

            if (int.Parse(dataTable.Rows[0]["RowCounts"].ToString()) == 0)
            {
                throw new NotFoundException();
            }
        }
    }
}
