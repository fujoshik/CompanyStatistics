using AutoMapper;
using CompanyStatistics.Domain.Abstraction.Repositories;
using CompanyStatistics.Domain.Constants;
using CompanyStatistics.Domain.DTOs.Company;
using CompanyStatistics.Domain.Exceptions;
using CompanyStatistics.Domain.Extensions;
using CompanyStatistics.Infrastructure.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace CompanyStatistics.Infrastructure.Repositories
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        private readonly IMapper _mapper;

        public CompanyRepository(IConfiguration configuration, IMapper mapper)
            : base(configuration)
        {
            TableName = "Companies";
            _mapper = mapper;
        }

        protected override TOutput DataRowToEntity<TOutput>(DataRow dataRow)
        {
            var result = new CompanyWithoutIndustryDto()
            {
                Id = dataRow["Id"].ToString(),
                CompanyIndex = int.Parse(dataRow["CompanyIndex"].ToString()),
                Name = dataRow["Name"].ToString(),
                Website = dataRow["Website"].ToString(),
                Country = dataRow["Country"].ToString(),
                Description = dataRow["Description"].ToString(),
                Founded = int.Parse(dataRow["Founded"].ToString()),
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

        private async Task<int> CountCompaniesAsync()
        {
            await CreateDbIfNotExist();

            var dataTable = new DataTable();
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(SqlQueryConstants.COUNT_COMPANIES, connection);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    dataTable.Load(reader);
                }
            }

            if (dataTable.Rows.Count > 0)
            {
                return int.Parse(dataTable.Rows[0]["Count"].ToString());
            }

            return 0;
        }

        public async Task<CompanyWithoutIndustryDto> InsertAsync(CompanyWithouIndustryRequestDto entity)
        {
            var index = await CountCompaniesAsync();
            entity.CompanyIndex = index + 1;

            return await base.InsertAsync<CompanyWithouIndustryRequestDto, CompanyWithoutIndustryDto>(entity);
        }

        public async Task BulkInsertAsync(List<CompanyRequestDto> companies)
        {
            using (SqlBulkCopy sqlBulk = new SqlBulkCopy(_dbConnectionString))
            {
                sqlBulk.DestinationTableName = "dbo.Companies";

                sqlBulk.ColumnMappings.Add(nameof(CompanyRequestDto.Id), "Id");
                sqlBulk.ColumnMappings.Add(nameof(CompanyRequestDto.CompanyIndex), "CompanyIndex");
                sqlBulk.ColumnMappings.Add(nameof(CompanyRequestDto.Name), "Name");
                sqlBulk.ColumnMappings.Add(nameof(CompanyRequestDto.Website), "Website");
                sqlBulk.ColumnMappings.Add(nameof(CompanyRequestDto.Country), "Country");
                sqlBulk.ColumnMappings.Add(nameof(CompanyRequestDto.Description), "Description");
                sqlBulk.ColumnMappings.Add(nameof(CompanyRequestDto.Founded), "Founded");
                sqlBulk.ColumnMappings.Add(nameof(CompanyRequestDto.NumberOfEmployees), "NumberOfEmployees");
                sqlBulk.ColumnMappings.Add(nameof(CompanyRequestDto.IsDeleted), "IsDeleted");

                await sqlBulk.WriteToServerAsync(companies.ToDataTable());
            }         
        }

        public async Task<int> GetCompaniesCountByDateAsync(DateTime date)
        {
            await CreateDbIfNotExist();

            var dataTable = new DataTable();

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(SqlQueryConstants.GET_COMPANIES_COUNT_BY_DATE, connection);
                cmd.Parameters.Add(new SqlParameter("@Date", date));

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    dataTable.Load(reader);
                }
            }

            if (dataTable.Rows.Count > 0)
            {
                return int.Parse(dataTable.Rows[0]["Count"].ToString());
            }

            return 0;
        }

        public async Task<CompanyWithoutIndustryDto> GetCompanyByNameAsync(string name)
        {
            await CreateDbIfNotExist();

            var dataTable = new DataTable();

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(SqlQueryConstants.GET_COMPANY_BY_NAME, connection);
                cmd.Parameters.Add(new SqlParameter("@Name", name));

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    dataTable.Load(reader);
                }
            }

            if (dataTable.Rows.Count == 0)
            {
                throw new NotFoundException();
            }

            return DataRowToEntity<CompanyWithoutIndustryDto>(dataTable.Rows[0]);

        }

        public async Task<List<CompanyWithoutIndustryDto>> GetTopNCompaniesByEmployeeCountAndDateAsync(int n, DateTime? date = null)
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
            return DataTableToCollection<CompanyWithoutIndustryDto>(dataTable);
        }

        public async Task<int> CountEmployeesByIndustryAsync(string industry)
        {
            await CreateDbIfNotExist();

            int result = 0;
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

            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    if (row != null)
                    {
                        result += int.Parse(row["NumberOfEmployees"].ToString());
                    }
                }
            }

            return result;
        }

        public async Task<List<CompanyWithoutIndustryDto>> GroupCompaniesByCountryAndIndustryAsync(string country, string industry)
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
                    cmd.Parameters.Add(new SqlParameter("@Country", $"%{country.ToLower()}%"));
                    cmd.Parameters.Add(new SqlParameter("@Industry", $"%{industry.ToLower()}%"));
                }
                else if (country != null)
                {
                    cmd = new SqlCommand(SqlQueryConstants.GROUP_COMPANIES_BY_COUNTRY, connection);
                    cmd.Parameters.Add(new SqlParameter("@Country", $"%{country.ToLower()}%"));
                }
                else if (industry != null)
                {
                    cmd = new SqlCommand(SqlQueryConstants.GROUP_COMPANIES_BY_INDUSTRY, connection);
                    cmd.Parameters.Add(new SqlParameter("@Industry", $"%{industry.ToLower()}%"));
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
            return DataTableToCollection<CompanyWithoutIndustryDto>(dataTable);
        }

        public async Task<HashSet<string>> GetAllCompanyIdsAsync()
        {
            var result = new HashSet<string>();
            var dataTable = new DataTable();

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(SqlQueryConstants.GET_ALL_COMPANY_IDS, connection);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    dataTable.Load(reader);
                }
            }

            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    if (row != null)
                    {
                        result.Add(row["Id"].ToString());
                    }
                }
            }
            
            return result;
        }

        public async Task DeleteCompanyAsync(string id)
        {
            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(SqlQueryConstants.DELETE_COMPANY_INDUSTRIES_BY_COMPANYID, connection);
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                await cmd.ExecuteNonQueryAsync();
            }

            await base.DeleteAsync(id);
        }
    }
}
