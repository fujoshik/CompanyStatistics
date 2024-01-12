namespace CompanyStatistics.Domain.Extensions
{
    public static class StringExtensions
    {
        public static void ReplaceQuotes(this string value)
        {
            value.Replace("'", "''").Replace("\"", "");
        }
    }
}
