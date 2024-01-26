using CompanyStatistics.Domain.DTOs.CompanyIndustry;
using CompanyStatistics.Domain.DTOs.Industry;

namespace CompanyStatistics.Domain.Abstraction.Repositories
{
    public interface ICompanyIndustriesRepository : IBaseRepository
    {
        Task BulkInsertAsync(List<CompanyIndustryRequestDto> companyIndustries);
        Task<List<IndustryResponseDto>> GetIndustriesByCompanyIdAsync(string companyId);
        Task DeleteByCompanyIdAndIndustryNameAsync(string companyId, string industryName);
    }
}
