using CompanyStatistics.Infrastructure.Mapper;

namespace CompanyStatistics.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCompanyStatisticsAutomapper(this IServiceCollection services)
            => services.AddAutoMapper(mc =>
            {
                mc.AddProfile(new CompanyProfile());
            });
    }
}
