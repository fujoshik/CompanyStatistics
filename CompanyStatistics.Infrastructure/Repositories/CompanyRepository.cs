using CompanyStatistics.Domain.Abstraction.Repositories;
using CompanyStatistics.Domain.Constants;
using CompanyStatistics.Domain.DTOs.Company;
using CompanyStatistics.Domain.Extensions;
using CompanyStatistics.Infrastructure.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text;

namespace CompanyStatistics.Infrastructure.Repositories
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(IConfiguration configuration)
            : base(configuration)
        {
            this.TableName = "Companies";
        }

        protected override TOutput DataRowToEntity<TOutput>(DataRow dataRow)
        {
            var result = new CompanyResponseDto()
            {
                Id = dataRow["Id"].ToString(),
                CompanyIndex = int.Parse(dataRow["CompanyIndex"].ToString()),
                Name = dataRow["Name"].ToString(),
                Website = dataRow["Website"].ToString(),
                Country = dataRow["Country"].ToString(),
                Description = dataRow["Description"].ToString(),
                Founded = int.Parse(dataRow["Founded"].ToString()),
                Industry = dataRow["Industry"].ToString(),
                NumberOfEmployees = int.Parse(dataRow["NumberOfEmployees"].ToString()),
                IsDeleted = bool.Parse(dataRow["IsDeleted"].ToString()),
                DateRead = DateTime.Parse(dataRow["DateRead"].ToString())
            };

            return (TOutput)Convert.ChangeType(result, typeof(TOutput));
        }

        protected override List<TOutput> DataTableToCollection<TOutput>(DataTable table)
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

        public async Task BulkInsertAsync(List<CompanyRequestDto> companies)
        {
            var properties = typeof(CompanyRequestDto).GetProperties().Where(x => x.CanRead).ToArray();

            var columnNames = GenerateColumnsForInsert(properties);

            var cmdText = companies.Aggregate(new StringBuilder(),
                (sb, entity) => sb.AppendLine(@$"
                    INSERT INTO {TableName} ({columnNames})
                    VALUES('{entity.Id}', '{entity.CompanyIndex}', '{entity.Name.ReplaceQuotes}', '{entity.Website.ReplaceQuotes}', 
                    '{entity.Country.ReplaceQuotes}', '{entity.Description.ReplaceQuotes}', {entity.Founded}, 
                    '{entity.Industry.ReplaceQuotes}', {entity.NumberOfEmployees}, {entity.IsDeleted})")
                );

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(cmdText.ToString(), connection);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task<List<CompanyResponseDto>> GetCompaniesByDate(DateTime date)
        {
            await CreateDbIfNotExist();

            var dataTable = new DataTable();

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(SqlQueryConstants.GET_COMPANIES_BY_DATE, connection);
                cmd.Parameters.Add(new SqlParameter("@Date", date));

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    dataTable.Load(reader);
                }
            }
            return DataTableToCollection<CompanyResponseDto>(dataTable);
        }

        public async Task<List<CompanyResponseDto>> GetTopNCompaniesByEmployeeCountAndDateAsync(int n, DateTime? date = null)
        {
            await CreateDbIfNotExist();

            var dataTable = new DataTable();

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();

                if (date != null)
                {
                    cmd = new SqlCommand(SqlQueryConstants.GET_TOP_N_COMPANIES_BY_EMPLOYEE_COUNT_AND_DATE, connection);
                    cmd.Parameters.Add(new SqlParameter("@N", n));
                    cmd.Parameters.Add(new SqlParameter("@Date", date));
                }
                else
                {
                    cmd = new SqlCommand(SqlQueryConstants.GET_TOP_N_COMPANIES_BY_EMPLOYEE_COUNT, connection);
                    cmd.Parameters.Add(new SqlParameter("@N", n));
                }

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    dataTable.Load(reader);
                }
            }
            return DataTableToCollection<CompanyResponseDto>(dataTable);
        }

        public async Task<int> CountEmployeesByIndustryAsync(string industry)
        {
            await CreateDbIfNotExist();

            var dataTable = new DataTable();
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(SqlQueryConstants.COUNT_EMPLOYEES_BY_INDUSTRY, connection);
                cmd.Parameters.Add(new SqlParameter("@Industry", $"%{industry.ToLower()}%"));

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    dataTable.Load(reader);
                }
            }
            int result = 0;

            if (dataTable.Rows.Count == 0)
            {
                return result;
            }

            foreach (DataRow row in dataTable.Rows)
            {
                if (row != null)
                {
                    result += int.Parse(row["NumberOfEmployees"].ToString());
                }
            }

            return result;
        }

        public async Task<List<CompanyResponseDto>> GroupCompaniesByCountryAndIndustryAsync(string country, string industry)
        {
            await CreateDbIfNotExist();

            var dataTable = new DataTable();

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                if (country != null && industry != null)
                {
                    cmd = new SqlCommand(SqlQueryConstants.GROUP_COMPANIES_BY_COUNTRY_AND_INDUSTRY, connection);
                    cmd.Parameters.Add(new SqlParameter("@Country", country));
                    cmd.Parameters.Add(new SqlParameter("@Industry", industry));
                }
                else if (country != null)
                {
                    cmd = new SqlCommand(SqlQueryConstants.GROUP_COMPANIES_BY_COUNTRY, connection);
                    cmd.Parameters.Add(new SqlParameter("@Country", country));
                }
                else if (industry != null)
                {
                    cmd = new SqlCommand(SqlQueryConstants.GROUP_COMPANIES_BY_INDUSTRY, connection);
                    cmd.Parameters.Add(new SqlParameter("@Industry", industry));
                }
                else
                {
                    cmd = new SqlCommand(SqlQueryConstants.RETURN_ALL_COMPANIES, connection);
                }

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    dataTable.Load(reader);
                }
            }
            return DataTableToCollection<CompanyResponseDto>(dataTable);
        }
    }
}
