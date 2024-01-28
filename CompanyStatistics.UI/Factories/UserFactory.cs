using CompanyStatistics.Domain.DTOs.User;
using CompanyStatistics.UI.Factories.Abstraction;

namespace CompanyStatistics.UI.Factories
{
    public class UserFactory : IUserFactory
    {
        public UserCreateWithoutAccountIdDto CreateUserCreateDto(string firstName, string lastName,
            string country, int age)
        {
            return new UserCreateWithoutAccountIdDto
            {
                FirstName = firstName,
                LastName = lastName,
                Country = country,
                Age = age
            };
        }

        public UserRequestDto CreateUserRequestDto(string firstName, string lastName,
            string country, int age, string accountId)
        {
            return new UserRequestDto
            {
                FirstName = firstName,
                LastName = lastName,
                Country = country,
                Age = age,
                AccountId = accountId
            };
        }
    }
}
