using CompanyStatistics.Domain.DTOs.Authentication;
using CompanyStatistics.UI.Constants;
using CompanyStatistics.UI.HttpClients.Abstraction;
using System.Net.Http.Json;

namespace CompanyStatistics.UI.HttpClients
{
    public class AuthenticateClient : BaseClient, IAuthenticateClient
    {
        public async Task<string> RegisterAsync(RegisterDto registerDto)
        {
            var response = await _httpClient.PostAsJsonAsync(UrlConstants.REGISTER_URL, registerDto);

            return (int)response.StatusCode + " " + response.ReasonPhrase;
        }

        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            var response = await _httpClient.PostAsJsonAsync(UrlConstants.LOGIN_URL, loginDto);

            if (!response.IsSuccessStatusCode)
            {
                return (int)response.StatusCode + " " + response.ReasonPhrase;
            }

            return await response.Content.ReadAsStringAsync();
        }
    }
}
