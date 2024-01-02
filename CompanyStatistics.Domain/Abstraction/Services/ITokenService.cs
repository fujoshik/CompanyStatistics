using CompanyStatistics.Domain.DTOs.Account;

namespace CompanyStatistics.Domain.Abstraction.Services
{
    public interface ITokenService
    {
        string GenerateJwtToken(AccountDto dto);
        bool ValidateToken(string authToken);
    }
}
