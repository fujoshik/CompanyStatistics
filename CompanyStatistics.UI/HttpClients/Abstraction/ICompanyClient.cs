using CompanyStatistics.Domain.DTOs.Company;

namespace CompanyStatistics.UI.HttpClients.Abstraction
{
    public interface ICompanyClient
    {
        Task<string> ReadDataAsync();
        Task<string> CreateAsync(CompanyCreateDto company, string bearer);
        Task<string> UpdateCompanyAsync(string id, CompanyCreateDto company, string bearer);
        Task<string> GetCompanyAsync(string id);
        Task<string> GetPageAsync(int pageNumber, int pageSize);
        Task<string> DeleteAsync(string id, string bearer);
    }
}
