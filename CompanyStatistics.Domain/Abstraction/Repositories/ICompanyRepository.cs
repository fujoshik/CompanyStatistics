using CompanyStatistics.Domain.DTOs.Company;

namespace CompanyStatistics.Domain.Abstraction.Repositories
{
    public interface ICompanyRepository : IBaseRepository
    {
        Task<HashSet<string>> GetAllCompanyIdsAsync();
        Task<CompanyWithoutIndustryDto> InsertAsync(CompanyWithouIndustryRequestDto entity);
        Task DeleteCompanyAsync(string id);
        Task<int> CountEmployeesByIndustryAsync(string industry);
        Task BulkInsertAsync(List<CompanyRequestDto> companies);
        Task<CompanyWithoutIndustryDto> GetCompanyByNameAsync(string name);
        Task<int> GetCompaniesCountByDateAsync(DateTime date);
        Task<List<CompanyWithoutIndustryDto>> GetTopNCompaniesByEmployeeCountAndDateAsync(int n, DateTime? date = null);
        Task<List<CompanyWithoutIndustryDto>> GroupCompaniesByCountryAndIndustryAsync(string country, string industry);
    }
}
