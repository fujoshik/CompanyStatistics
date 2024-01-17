namespace CompanyStatistics.Domain.Constants
{
    public class SqlQueryConstants
    {
        public const string USE_COMPANYSTATISTICSDB = "USE CompanyStatisticsDB;";

        public const string GET_ACCOUNTS_BY_EMAIL = $@"{USE_COMPANYSTATISTICSDB} SELECT * FROM Accounts WHERE Email = @Email AND IsDeleted = 0";

        public const string COUNT_EMPLOYEES_BY_INDUSTRY = $@"{USE_COMPANYSTATISTICSDB} SELECT * FROM Companies WHERE Industry LIKE @Industry AND IsDeleted = 0";

        public const string GET_TOP_N_COMPANIES_BY_EMPLOYEE_COUNT = $@"{USE_COMPANYSTATISTICSDB} SELECT TOP (@N) * FROM Companies WHERE IsDeleted = 0 ORDER BY NumberOfEmployees DESC";

        public const string GET_TOP_N_COMPANIES_BY_EMPLOYEE_COUNT_AND_DATE = $@"{USE_COMPANYSTATISTICSDB} SELECT TOP (@N) * FROM Companies WHERE IsDeleted = 0 AND CAST(DateRead AS date) = CAST(@Date AS date) ORDER BY NumberOfEmployees DESC";

        public const string GROUP_COMPANIES_BY_COUNTRY_AND_INDUSTRY = $@"{USE_COMPANYSTATISTICSDB} SELECT * FROM Companies WHERE Country = @Country AND Industry = @Industry AND IsDeleted = 0";

        public const string GROUP_COMPANIES_BY_COUNTRY = $@"{USE_COMPANYSTATISTICSDB} SELECT * FROM Companies WHERE Country = @Country AND IsDeleted = 0";

        public const string GROUP_COMPANIES_BY_INDUSTRY = $@"{USE_COMPANYSTATISTICSDB} SELECT * FROM Companies WHERE Industry = @Industry AND IsDeleted = 0";

        public const string RETURN_ALL_COMPANIES = $@"{USE_COMPANYSTATISTICSDB} SELECT * FROM Companies WHERE IsDeleted = 0";

        public const string GET_COMPANIES_BY_DATE = $@"{USE_COMPANYSTATISTICSDB} SELECT * FROM Companies WHERE IsDeleted = 0 AND CAST(DateRead AS date) = CAST(@Date AS date)";

        public const string GET_COMPANY_BY_NAME = $@"{USE_COMPANYSTATISTICSDB} SELECT * FROM Companies WHERE IsDeleted = 0 AND Name = @Name";

        public const string GET_ALL_COMPANY_IDS = $@"{USE_COMPANYSTATISTICSDB} SELECT Id FROM Companies";
    }
}
