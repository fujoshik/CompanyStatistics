using CompanyStatistics.Domain.DTOs.Account;

namespace CompanyStatistics.Domain.Abstraction.Repositories
{
    public interface IAccountRepository : IBaseRepository
    {
        Task<List<AccountDto>> GetAccountsByEmail(string email);
    }
}
