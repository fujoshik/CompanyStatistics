using CompanyStatistics.Domain.Abstraction.Repositories;
using CompanyStatistics.Domain.Constants;
using CompanyStatistics.Domain.DTOs.Industry;
using CompanyStatistics.Domain.Extensions;
using CompanyStatistics.Infrastructure.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace CompanyStatistics.Infrastructure.Repositories
{
    public class IndustryRepository : BaseRepository<Industry>, IIndustryRepository
    {
        public IndustryRepository(IConfiguration configuration)
           : base(configuration)
        {
            this.TableName = "Industries";
        }

        public async Task BulkInsertAsync(List<IndustryRequestDto> industries)
        {
            using (SqlBulkCopy sqlBulk = new SqlBulkCopy(_dbConnectionString))
            {
                sqlBulk.DestinationTableName = "dbo.Industries";

                sqlBulk.ColumnMappings.Add(nameof(IndustryRequestDto.Name), "Name");

                await sqlBulk.WriteToServerAsync(industries.ToDataTable());
            }
        }

        public async Task<HashSet<string>> GetAllIndustriesAsync()
        {
            var result = new HashSet<string>();
            var dataTable = new DataTable();

            using (var connection = new SqlConnection(_dbConnectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(SqlQueryConstants.GET_ALL_INDUSTRIES, connection);

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
                        result.Add(row["Name"].ToString());
                    }
                }
            }

            return result;
        }
    }
}
