namespace CompanyStatistics.UI.Constants
{
    public class UrlConstants
    {
        public const string BASE_URL = "https://localhost:7070/api/";

        public const string REGISTER_URL = $"{BASE_URL}authentication/register";

        public const string LOGIN_URL = $"{BASE_URL}authentication/login";

        public const string GET_COMPANY_URL = $"{BASE_URL}companies/";

        public const string GET_USER_URL = $"{BASE_URL}users/";

        public const string READ_DATA_URL = $"{GET_COMPANY_URL}read-data/";

        public const string COUNT_EMPLOYEES_BY_INDUSTRY_URL = $"{BASE_URL}statistics/employee-count-by-industry";

        public const string GET_TOP_N_COMPANIES_BY_EMPLOYEE_COUNT_URL = $"{BASE_URL}statistics/get-top-n-companies-by-employee-count";

        public const string GROUP_COMPANIES_BY_COUNTRY_AND_INDUSTRY_URL = $"{BASE_URL}statistics/group-companies-by-country-and-industry";

        public const string GENERATE_PDF_URL = $"{BASE_URL}pdfs/get-pdf-info-about-company";
    }
}
