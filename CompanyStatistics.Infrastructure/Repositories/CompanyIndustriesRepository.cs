using CompanyStatistics.Domain.Abstraction.Repositories;
using CompanyStatistics.Domain.Constants;
using CompanyStatistics.Domain.DTOs.CompanyIndustry;
using CompanyStatistics.Domain.DTOs.Industry;
using CompanyStatistics.Domain.Extensions;
using CompanyStatistics.Infrastructure.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace CompanyStatistics.Infrastructure.Repositories
{
    public class CompanyIndustriesRepository : BaseRepository<CompanyIndustry>, ICompanyIndustriesRepository
    {
        public CompanyIndustriesRepository(IConfiguration configuration)
           : base(configuration)
        {
            this.TableName = "Company_Industries";
        }

        protected override TOutput DataRowToEntity<TOutput>(DataRow dataRow)
        {
            var result = new CompanyIndustryResponseDto()
            {
                IndustryName = dataRow["IndustryName"].ToString()
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

        public async Task BulkInsertAsync(List<CompanyIndustryRequestDto> companyIndustries)
        {
            using (SqlBulkCopy sqlBulk = new SqlBulkCopy(_dbConnectionString))
            {
                sqlBulk.DestinationTableName = "dbo.Company_Industries";

                sqlBulk.ColumnMappings.Add(nameof(CompanyIndustryRequestDto.CompanyId), "CompanyId");
                sqlBulk.ColumnMappings.Add(nameof(CompanyIndustryRequestDto.IndustryName), "IndustryName");
                sqlBulk.ColumnMappings.Add(nameof(CompanyIndustryRequestDto.IsDeleted), "IsDeleted");

                await sqlBulk.WriteToServerAsync(companyIndustries.ToDataTable());
            }
        }

        public async Task<List<IndustryResponseDto>> GetIndustriesByCompanyIdAsync(string companyId)
        {
            await CreateDbIfNotExist();

            var dataTable = new DataTable();

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(SqlQueryConstants.GET_INDUSTRIES_BY_COMPANYID, connection);
                cmd.Parameters.Add(new SqlParameter("@CompanyId", companyId));

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    dataTable.Load(reader);
                }
            }
            var companyIndustries = DataTableToCollection<CompanyIndustryResponseDto>(dataTable);

            return companyIndustries
                .Select(x => new IndustryResponseDto { Name = x.IndustryName })
                .ToList();
        }
    }
}
