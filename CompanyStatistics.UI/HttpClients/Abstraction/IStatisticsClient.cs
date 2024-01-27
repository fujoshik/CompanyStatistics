using CompanyStatistics.Domain.DTOs.Company;

namespace CompanyStatistics.UI.HttpClients.Abstraction
{
    public interface IStatisticsClient
    {
        Task<string> CountEmployeesByIndustryAsync(string industry);
        Task<string> GetTopNCompaniesByEmployeeCountAsync(int n);
        Task<string> GroupCompaniesByCountryAndIndustryAsync(string country = null, string industry = null);
        Task<string> GeneratePdfAsync(string companyName, string bearer);
    }
}
