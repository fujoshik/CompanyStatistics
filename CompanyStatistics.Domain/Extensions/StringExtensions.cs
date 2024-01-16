namespace CompanyStatistics.Domain.Extensions
{
    public static class StringExtensions
    {
        public static string ReplaceQuotes(this string value)
        {
            return value.Replace("'", "''").Replace("\"", "");
        }
    }
}
