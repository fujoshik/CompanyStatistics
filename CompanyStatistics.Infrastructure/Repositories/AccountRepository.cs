using CompanyStatistics.Domain.Abstraction.Repositories;
using CompanyStatistics.Domain.DTOs.Account;
using CompanyStatistics.Domain.DTOs.Company;
using CompanyStatistics.Domain.Enums;
using CompanyStatistics.Infrastructure.Entities;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace CompanyStatistics.Infrastructure.Repositories
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(IConfiguration configuration)
            : base(configuration)
        {
            this.TableName = nameof(Account) + "s";
        }

        protected override TOutput DataRowToEntity<TOutput>(DataRow dataRow)
        {
            var result = new AccountResponseDto()
            {
                Id = dataRow["Id"].ToString(),
                Email = dataRow["Email"].ToString(),
                Role = (Role)int.Parse(dataRow["Role"].ToString())
            };

            return (TOutput)Convert.ChangeType(result, typeof(TOutput));
        }
    }
}
