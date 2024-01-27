using CompanyStatistics.Domain.DTOs.Company;
using CompanyStatistics.Domain.Pagination;
using CompanyStatistics.UI.Constants;
using CompanyStatistics.UI.HttpClients.Abstraction;
using System.Net.Http.Json;

namespace CompanyStatistics.UI.HttpClients
{
    public class CompanyClient : BaseClient, ICompanyClient
    {
        public async Task<string> ReadDataAsync()
        {
            var response = await _httpClient.GetAsync(UrlConstants.READ_DATA_URL);

            return (int)response.StatusCode + " " + response.ReasonPhrase;
        }

        public async Task<string> CreateAsync(CompanyCreateDto company, string bearer)
        {
            AddAuthorizationHeader(bearer);

            var response = await _httpClient.PostAsJsonAsync(UrlConstants.GET_COMPANY_URL, company);

            return (int)response.StatusCode + " " + response.ReasonPhrase;
        }

        public async Task<string> UpdateCompanyAsync(string id, CompanyCreateDto company, string bearer)
        {
            AddAuthorizationHeader(bearer);

            var response = await _httpClient.PutAsJsonAsync(UrlConstants.GET_COMPANY_URL + id, company);

            return (int)response.StatusCode + " " + response.ReasonPhrase;
        }

        public async Task<string> GetCompanyAsync(string id)
        {
            var response = await _httpClient.GetAsync(UrlConstants.GET_COMPANY_URL + id);

            if (!response.IsSuccessStatusCode)
            {
                return (int)response.StatusCode + " " + response.ReasonPhrase;
            }

            var result = await response.Content.ReadFromJsonAsync<CompanyResponseDto>();

            return result.ToString();
        }

        public async Task<string> GetPageAsync(int pageNumber, int pageSize)
        {
            var query = $"?PageNumber={pageNumber}&PageSize={pageSize}";

            var getRoute = UrlConstants.GET_COMPANY_URL + query;

            var response = await _httpClient.GetAsync(getRoute);

            if (!response.IsSuccessStatusCode)
            {
                return (int)response.StatusCode + " " + response.ReasonPhrase;
            }

            var result = await response.Content.ReadFromJsonAsync<PaginatedResult<CompanyResponseDto>>();

            return string.Join("; ", result.Content);
        }

        public async Task<string> DeleteAsync(string id, string bearer)
        {
            AddAuthorizationHeader(bearer);

            var response = await _httpClient.DeleteAsync(UrlConstants.GET_COMPANY_URL + id);

            return (int)response.StatusCode + " " + response.ReasonPhrase;
        }
    }
}
