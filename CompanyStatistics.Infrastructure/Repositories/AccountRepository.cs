using CompanyStatistics.Domain.Abstraction.Repositories;
using CompanyStatistics.Domain.Constants;
using CompanyStatistics.Domain.DTOs.Account;
using CompanyStatistics.Domain.Enums;
using CompanyStatistics.Infrastructure.Entities;
using Microsoft.Data.SqlClient;
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

        public async Task<List<AccountDto>> GetAccountsByEmail(string email)
        {
            var dataTable = new DataTable();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(SqlQueryConstants.GET_ACCOUNTS_BY_EMAIL, connection);
                cmd.Parameters.Add(new SqlParameter("@Email", email));
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    dataTable.Load(reader);
                }
            }

            return dataTable
                .AsEnumerable()
                .Select(x => new AccountDto
                {
                    Id = x["Id"].ToString(),
                    Email = x["Email"].ToString(),
                    PasswordHash = x["PasswordHash"].ToString(),
                    PasswordSalt = x["PasswordSalt"].ToString(),
                    Role = (Role)int.Parse(x["Role"].ToString())
                })
                .ToList();
        }
    }
}
