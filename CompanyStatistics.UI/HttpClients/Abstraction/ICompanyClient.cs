using CompanyStatistics.Domain.DTOs.Company;

namespace CompanyStatistics.UI.HttpClients.Abstraction
{
    public interface ICompanyClient
    {
        Task<string> ReadDataAsync();
        Task<string> CreateAsync(CompanyCreateDto company, string bearer);
        Task<string> UpdateCompanyAsync(string id, CompanyWithoutIdDto company, string bearer);
        Task<string> GetCompanyAsync(string id, string bearer);
        Task<string> GetPageAsync(int pageNumber, int pageSize, string bearer);
        Task<string> DeleteAsync(string id, string bearer);
    }
}
