using CompanyStatistics.Domain.DTOs.Company;

namespace CompanyStatistics.UI.HttpClients.Abstraction
{
    public interface IStatisticsClient
    {
        Task<int> CountEmployeesByIndustryAsync(string industry);
        Task<CompanyResponseDto> GetTopNCompaniesByEmployeeCountAsync(int n);
        Task<List<CompanyResponseDto>> GroupCompaniesByCountryAndIndustryAsync(string country = null, string industry = null);
        Task<string> GeneratePdfAsync(string companyName, string bearer);
    }
}
