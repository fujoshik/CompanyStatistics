using CompanyStatistics.Domain.DTOs.Company;

namespace CompanyStatistics.Domain.Abstraction.Repositories
{
    public interface ICompanyRepository : IBaseRepository
    {
        Task<int> CountEmployeesByIndustryAsync(string industry);
        Task BulkInsertAsync(List<CompanyRequestDto> companies);
        Task<List<CompanyResponseDto>> GetCompaniesByDate(DateTime date);
        Task<List<CompanyResponseDto>> GetTopNCompaniesByEmployeeCountAndDateAsync(int n, DateTime? date = null);
        Task<List<CompanyResponseDto>> GroupCompaniesByCountryAndIndustryAsync(string country, string industry);
    }
}
