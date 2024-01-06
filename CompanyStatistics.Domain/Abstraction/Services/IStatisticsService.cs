using CompanyStatistics.Domain.DTOs.Company;

namespace CompanyStatistics.Domain.Abstraction.Services
{
    public interface IStatisticsService
    {
        Task<int> CountEmployeesByIndustryAsync(string industry);
        Task<List<CompanyResponseDto>> GetTopNCompaniesByEmployeeCountAsync(int n);
        Task<List<CompanyResponseDto>> GroupCompaniesByCountryAndIndustryAsync(string country, string industry);
    }
}
