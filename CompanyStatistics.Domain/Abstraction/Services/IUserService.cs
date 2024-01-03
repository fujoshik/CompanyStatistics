using CompanyStatistics.Domain.DTOs.Authentication;
using CompanyStatistics.Domain.DTOs.User;
using CompanyStatistics.Domain.Pagination;

namespace CompanyStatistics.Domain.Abstraction.Services
{
    public interface IUserService
    {
        Task<UserResponseDto> CreateAsync(RegisterDto registerDto, string accountId);
        Task<UserResponseDto> UpdateAsync(string id, UserWithoutIdDto user);
        Task<UserResponseDto> GetByIdAsync(string id);
        Task<PaginatedResult<UserResponseDto>> GetPageAsync(PagingInfo pagingInfo);
        Task DeleteAsync(string id);
    }
}
