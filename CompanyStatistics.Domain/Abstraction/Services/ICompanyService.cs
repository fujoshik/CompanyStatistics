using CompanyStatistics.Domain.DTOs.Company;
using CompanyStatistics.Domain.Pagination;

namespace CompanyStatistics.Domain.Abstraction.Services
{
    public interface ICompanyService
    {
        Task<CompanyResponseDto> CreateAsync(CompanyRequestDto companyDto);
        Task<CompanyResponseDto> UpdateAsync(string id, CompanyWithoutIdDto company);
        Task<CompanyResponseDto> GetByIdAsync(string id);
        Task<PaginatedResult<CompanyResponseDto>> GetPageAsync(PagingInfo pagingInfo);
        Task DeleteAsync(string id);
        Task<CompanyResponseDto> GetCompanyByNameAsync(string companyName);
    }
}
