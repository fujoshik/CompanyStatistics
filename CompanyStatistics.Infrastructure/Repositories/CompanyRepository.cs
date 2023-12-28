using CompanyStatistics.Domain.Abstraction.Repositories;
using CompanyStatistics.Domain.Enums;
using CompanyStatistics.Infrastructure.Entities;
using Microsoft.Extensions.Configuration;
using System.Data;

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
            var result = new AccountResponseDto()
            {
                Id = Guid.Parse(dataRow["Id"].ToString()),
                Email = dataRow["Email"].ToString(),
                WalletId = Guid.Parse(dataRow["WalletId"].ToString()),
                Role = (Role)int.Parse(dataRow["Role"].ToString()),
                DateToDelete = dataRow["DateToDelete"].ToString() == null ? null : DateTime.Parse(dataRow["DateToDelete"].ToString())
            };

            return (TOutput)Convert.ChangeType(result, typeof(TOutput));
        }
    }
}
