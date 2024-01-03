using CompanyStatistics.Domain.Abstraction.Repositories;
using CompanyStatistics.Domain.DTOs.User;
using CompanyStatistics.Infrastructure.Entities;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace CompanyStatistics.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IConfiguration configuration)
            : base(configuration)
        {
            this.TableName = nameof(User) + "s";
        }

        protected override TOutput DataRowToEntity<TOutput>(DataRow dataRow)
        {
            var result = new UserResponseDto()
            {
                Id = dataRow["Id"].ToString(),
                Age = int.Parse(dataRow["Age"].ToString()),
                FirstName = dataRow["FirstName"].ToString(),
                LastName = dataRow["LastName"].ToString(),
                AccountId = dataRow["AccountId"].ToString(),
                Country = dataRow["Country"].ToString(),
                IsDeleted = bool.Parse(dataRow["IsDeleted"].ToString())
            };

            return (TOutput)Convert.ChangeType(result, typeof(TOutput));
        }
    }
}
