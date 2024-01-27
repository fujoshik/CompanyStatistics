using CompanyStatistics.UI.Constants;
using System.Net.Http.Headers;

namespace CompanyStatistics.UI.HttpClients
{
    public class BaseClient
    {
        protected HttpClient _httpClient;

        public BaseClient()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(UrlConstants.BASE_URL)
            };
        }

        protected void AddAuthorizationHeader(string bearerToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization
                         = new AuthenticationHeaderValue("Bearer", bearerToken);
        }
    }
}
