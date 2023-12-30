using CompanyStatistics.Domain.DTOs.Account;
using CompanyStatistics.Domain.Pagination;

namespace CompanyStatistics.Domain.Abstraction.Services
{
    public interface IAccountService
    {
        Task<AccountResponseDto> CreateAsync(AccountRequestDto account);
        Task<AccountResponseDto> UpdateAsync(string id, AccountRequestDto account);
        Task<AccountResponseDto> GetByIdAsync(string id);
        Task<PaginatedResult<AccountResponseDto>> GetPageAsync(PagingInfo pagingInfo);
        Task DeleteAsync(string id);
    }
}
