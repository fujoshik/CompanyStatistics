using CompanyStatistics.Domain.DTOs.Company;
using CompanyStatistics.Domain.Pagination;

namespace CompanyStatistics.Domain.Abstraction.Services
{
    public interface ICompanyService
    {
        Task<CompanyResponseDto> CreateAsync(CompanyCreateDto company);
        Task<CompanyResponseDto> UpdateAsync(string id, CompanyCreateDto company);
        Task<CompanyResponseDto> GetByIdAsync(string id);
        Task<PaginatedResult<CompanyResponseDto>> GetPageAsync(PagingInfo pagingInfo);
        Task DeleteAsync(string id);
        Task<CompanyResponseDto> GetCompanyByNameAsync(string companyName);
        Task<CompanyResponseDto> AssignIndustriesAsync(CompanyWithoutIndustryDto companyWithoutIndustry);
    }
}
