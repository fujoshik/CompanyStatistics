using CompanyStatistics.Domain.DTOs.Authentication;

namespace CompanyStatistics.UI.HttpClients.Abstraction
{
    public interface IAuthenticateClient
    {
        Task<string> RegisterAsync(RegisterDto registerDto);
        Task<string> LoginAsync(LoginDto loginDto);
    }
}
