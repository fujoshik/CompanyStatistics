using CompanyStatistics.Domain.DTOs.Authentication;

namespace CompanyStatistics.Domain.Abstraction.Services
{
    public interface IAuthenticationService
    {
        Task<string> LoginAsync(LoginDto loginDto);
        Task RegisterAccountAsync(RegisterDto registerDto);
    }
}
