namespace CompanyStatistics.Domain.Constants
{
    public class CronExpressionConstants
    {
        public const string EVERY_MINUTE = "0 * * ? * *";

        public const string EVERY_SIX_HOURS = "0 0 */6 ? * *";

        public const string EVERY_DAY_AT_MIDNIGHT = "0 0 0 * * ?";
    }
}
