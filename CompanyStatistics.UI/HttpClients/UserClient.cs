using CompanyStatistics.Domain.DTOs.User;
using CompanyStatistics.Domain.Pagination;
using CompanyStatistics.UI.Constants;
using CompanyStatistics.UI.HttpClients.Abstraction;
using System.Net.Http.Json;

namespace CompanyStatistics.UI.HttpClients
{
    public class UserClient : BaseClient, IUserClient
    {
        public async Task<string> CreateAsync(UserRequestDto user, string bearer)
        {
            AddAuthorizationHeader(bearer);

            var response = await _httpClient.PostAsJsonAsync(UrlConstants.GET_USER_URL, user);

            return (int)response.StatusCode + " " + response.ReasonPhrase;
        }

        public async Task<string> UpdateAsync(string id, UserCreateWithoutAccountIdDto user, string bearer)
        {
            AddAuthorizationHeader(bearer);

            var response = await _httpClient.PutAsJsonAsync(UrlConstants.GET_USER_URL + id, user);

            return (int)response.StatusCode + " " + response.ReasonPhrase;
        }

        public async Task<string> GetUserAsync(string id, string bearer)
        {
            AddAuthorizationHeader(bearer);

            var response = await _httpClient.GetAsync(UrlConstants.GET_USER_URL + id);

            if (!response.IsSuccessStatusCode)
            {
                return (int)response.StatusCode + " " + response.ReasonPhrase;
            }

            var result = await response.Content.ReadFromJsonAsync<UserResponseDto>();

            return result.ToString();
        }

        public async Task<string> GetPageAsync(int pageNumber, int pageSize, string bearer)
        {
            AddAuthorizationHeader(bearer);

            var query = $"?PageNumber={pageNumber}&PageSize={pageSize}";

            var getRoute = UrlConstants.GET_USER_URL + query;

            var response = await _httpClient.GetAsync(getRoute);

            if (!response.IsSuccessStatusCode)
            {
                return (int)response.StatusCode + " " + response.ReasonPhrase;
            }

            var result = await response.Content.ReadFromJsonAsync<PaginatedResult<UserResponseDto>>();

            return string.Join("; ", result.Content);
        }

        public async Task<string> DeleteAsync(string id, string bearer)
        {
            AddAuthorizationHeader(bearer);

            var response = await _httpClient.DeleteAsync(UrlConstants.GET_USER_URL + id);

            return (int)response.StatusCode + " " + response.ReasonPhrase;
        }
    }
}
