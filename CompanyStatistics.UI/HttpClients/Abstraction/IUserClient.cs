using CompanyStatistics.Domain.DTOs.User;

namespace CompanyStatistics.UI.HttpClients.Abstraction
{
    public interface IUserClient
    {
        Task<string> CreateAsync(UserRequestDto user, string bearer);
        Task<string> UpdateAsync(string id, UserRequestDto company, string bearer);
        Task<string> GetUserAsync(string id);
        Task<string> GetPageAsync(int pageNumber, int pageSize);
        Task<string> DeleteAsync(string id, string bearer);
    }
}
