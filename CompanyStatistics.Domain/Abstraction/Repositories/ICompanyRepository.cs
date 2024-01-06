using CompanyStatistics.Domain.DTOs.Company;

namespace CompanyStatistics.Domain.Abstraction.Repositories
{
    public interface ICompanyRepository : IBaseRepository
    {
        Task<int> CountEmployeesByIndustryAsync(string industry);
        Task<List<CompanyResponseDto>> GetTopNCompaniesByEmployeeCountAsync(int n);
        Task<List<CompanyResponseDto>> GroupCompaniesByCountryAndIndustryAsync(string country, string industry);
    }
}
