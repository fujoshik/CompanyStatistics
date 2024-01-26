using CompanyStatistics.Domain.DTOs.Company;

namespace CompanyStatistics.Domain.Abstraction.Services
{
    public interface IStatisticsService
    {
        Task<int> CountEmployeesByIndustryAsync(string industry);
        Task<int> GetCompaniesCountByDateAsync(DateTime date);
        Task<List<CompanyResponseDto>> GetTopNCompaniesByEmployeeCountAndDateAsync(int n, DateTime? date = null);
        Task<List<CompanyResponseDto>> GroupCompaniesByCountryAndIndustryAsync(string country, string industry);
    }
}
