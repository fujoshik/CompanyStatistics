using CompanyStatistics.Domain.Jobs;
using CompanyStatistics.Domain.Services;
using CompanyStatistics.Infrastructure.Mapper;
using Quartz;

namespace CompanyStatistics.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCompanyStatisticsAutomapper(this IServiceCollection services)
            => services.AddAutoMapper(mc =>
            {
                mc.AddProfile(new CompanyProfile());
                mc.AddProfile(new AccountProfile());
                mc.AddProfile(new UserProfile());
            });

        public static IServiceCollection AddQuartzConfiguration(this IServiceCollection services)
        {
            services.AddQuartz(q =>
            {
                var dailyStatisticsJob = JobKey.Create(nameof(DailyStatisticsJob));
                q.AddJob<DailyStatisticsJob>(dailyStatisticsJob)
                    .AddTrigger(t => t
                        .ForJob(dailyStatisticsJob)
                        .WithCronSchedule("0 */3 * ? * *"));
            });

            services.AddQuartzHostedService();

            return services;
        }
    }
}
