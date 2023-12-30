using CompanyStatistics.Domain.Abstraction.Repositories;
using CompanyStatistics.Domain.DTOs.Company;
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
            this.TableName = "Companies";
        }

        protected override TOutput DataRowToEntity<TOutput>(DataRow dataRow)
        {
            var result = new AccountResponseDto()
            {
                Id = dataRow["Id"].ToString(),
                CompanyIndex = int.Parse(dataRow["CompanyIndex"].ToString()),
                Name = dataRow["Name"].ToString(),
                Website = dataRow["Website"].ToString(),
                Country = dataRow["Country"].ToString(),
                Description = dataRow["Description"].ToString(),
                Founded = int.Parse(dataRow["Founded"].ToString()),
                Industry = dataRow["Industry"].ToString(),
                NumberOfEmployees = int.Parse(dataRow["NumberOfEmployees"].ToString())
            };

            return (TOutput)Convert.ChangeType(result, typeof(TOutput));
        }
    }
}
