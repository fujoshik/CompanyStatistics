namespace CompanyStatistics.Domain.Constants
{
    public class SqlQueryConstants
    {
        public const string USE_COMPANYSTATISTICSDB = "USE CompanyStatisticsDB;";

        public const string GET_ACCOUNTS_BY_EMAIL = $@"{USE_COMPANYSTATISTICSDB} SELECT * FROM Accounts WHERE Email = @Email";

        public const string COUNT_EMPLOYEES_BY_INDUSTRY = $@"{USE_COMPANYSTATISTICSDB} SELECT * FROM Companies WHERE Industry LIKE @Industry";

        public const string GET_TOP_N_COMPANIES_BY_EMPLOYEE_COUNT = $@"{USE_COMPANYSTATISTICSDB} SELECT TOP (@N) * FROM Companies ORDER BY NumberOfEmployees DESC";

        public const string GROUP_COMPANIES_BY_COUNTRY_AND_INDUSTRY = $@"{USE_COMPANYSTATISTICSDB} SELECT * FROM Companies WHERE Country = @Country AND Industry = @Industry";

        public const string GROUP_COMPANIES_BY_COUNTRY = $@"{USE_COMPANYSTATISTICSDB} SELECT * FROM Companies WHERE Country = @Country";

        public const string GROUP_COMPANIES_BY_INDUSTRY = $@"{USE_COMPANYSTATISTICSDB} SELECT * FROM Companies WHERE Industry = @Industry";

        public const string RETURN_ALL_COMPANIES = $@"{USE_COMPANYSTATISTICSDB} SELECT * FROM Companies";
    }
}
