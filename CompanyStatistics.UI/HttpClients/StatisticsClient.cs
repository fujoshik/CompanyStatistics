using CompanyStatistics.Domain.DTOs.Company;
using CompanyStatistics.UI.Constants;
using CompanyStatistics.UI.HttpClients.Abstraction;
using System.Net.Http.Json;

namespace CompanyStatistics.UI.HttpClients
{
    public class StatisticsClient : BaseClient, IStatisticsClient
    {
        public async Task<string> CountEmployeesByIndustryAsync(string industry)
        {
            var route = UrlConstants.COUNT_EMPLOYEES_BY_INDUSTRY_URL + $"?industry={industry}";

            var response = await _httpClient.GetAsync(route);

            if (!response.IsSuccessStatusCode)
            {
                return (int)response.StatusCode + " " + response.ReasonPhrase;
            }

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetTopNCompaniesByEmployeeCountAsync(int n)
        {
            var route = UrlConstants.GET_TOP_N_COMPANIES_BY_EMPLOYEE_COUNT_URL + $"?n={n}";

            var response = await _httpClient.GetAsync(route);

            if (!response.IsSuccessStatusCode)
            {
                return (int)response.StatusCode + " " + response.ReasonPhrase;
            }

            var result = await response.Content.ReadFromJsonAsync<CompanyResponseDto>();
            return result.ToString();
        }

        public async Task<string> GroupCompaniesByCountryAndIndustryAsync(string country = null, string industry = null)
        {
            var route = UrlConstants.GET_TOP_N_COMPANIES_BY_EMPLOYEE_COUNT_URL + $"?country={country}&industry={industry}";

            var response = await _httpClient.GetAsync(route);

            if (!response.IsSuccessStatusCode)
            {
                return (int)response.StatusCode + " " + response.ReasonPhrase;
            }

            var result = await response.Content.ReadFromJsonAsync<List<CompanyResponseDto>>();

            return string.Join("; ", result);
        }

        public async Task<string> GeneratePdfAsync(string companyName, string bearer)
        {
            AddAuthorizationHeader(bearer);

            var route = UrlConstants.GENERATE_PDF_URL + $"?companyName={companyName}";

            var response = await _httpClient.GetAsync(route);

            return (int)response.StatusCode + " " + response.ReasonPhrase;
        }
    }
}
