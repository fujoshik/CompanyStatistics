using CompanyStatistics.API.Middleware;

namespace CompanyStatistics.API.Extensions
{
    public static class MiddlewareExtensions
    {
        public static void ConfigureSafelistMiddleware(this WebApplication app, string safelist)
        {
            app.UseMiddleware<AdminSafelistMiddleware>(safelist);
        }

        public static void ConfigureCustomExceptionMiddleware(this WebApplication app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }

        public static void ConfigureCustomHeaderMiddleware(this WebApplication app)
        {
            app.UseMiddleware<CustomHeaderMiddleware>();
        }
    }
}
