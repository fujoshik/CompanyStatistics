using CompanyStatistics.Domain.DTOs.User;
using CompanyStatistics.Domain.Pagination;

namespace CompanyStatistics.Domain.Abstraction.Services
{
    public interface IUserService
    {
        Task<UserResponseDto> CreateAsync(UserRequestDto user);
        Task<UserResponseDto> UpdateAsync(string id, UserRequestDto user);
        Task<UserResponseDto> GetByIdAsync(string id);
        Task<PaginatedResult<UserResponseDto>> GetPageAsync(PagingInfo pagingInfo);
        Task DeleteAsync(string id);
    }
}
