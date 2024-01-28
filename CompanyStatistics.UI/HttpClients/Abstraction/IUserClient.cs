using CompanyStatistics.Domain.DTOs.User;

namespace CompanyStatistics.UI.HttpClients.Abstraction
{
    public interface IUserClient
    {
        Task<string> CreateAsync(UserRequestDto user, string bearer);
        Task<string> UpdateAsync(string id, UserCreateDto user, string bearer);
        Task<string> GetUserAsync(string id, string bearer);
        Task<string> GetPageAsync(int pageNumber, int pageSize, string bearer);
        Task<string> DeleteAsync(string id, string bearer);
    }
}
