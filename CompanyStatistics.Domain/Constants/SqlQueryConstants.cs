namespace CompanyStatistics.Domain.Constants
{
    public class SqlQueryConstants
    {
        public const string USE_COMPANYSTATISTICSDB = "USE CompanyStatisticsDB;";

        public const string GET_ACCOUNTS_BY_EMAIL = $@"{USE_COMPANYSTATISTICSDB} SELECT * FROM Accounts WHERE Email = @Email";
    }
}
