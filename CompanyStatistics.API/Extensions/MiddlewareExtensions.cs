using CompanyStatistics.API.Middleware;

namespace CompanyStatistics.API.Extensions
{
    public static class MiddlewareExtensions
    {
        public static void ConfigureSafelistMiddleware(this WebApplication app, string safelist)
        {
            app.UseMiddleware<AdminSafelistMiddleware>(safelist);
        }
    }
}
