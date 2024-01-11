﻿using CompanyStatistics.Domain.Constants;
using CompanyStatistics.Domain.Jobs;
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
                        .WithCronSchedule(CronExpressionConstants.EVERY_MINUTE));

                var readDataJob = JobKey.Create(nameof(ReadDataJob));
                q.AddJob<ReadDataJob>(readDataJob)
                    .AddTrigger(t => t
                        .ForJob(readDataJob)
                        .WithCronSchedule(CronExpressionConstants.EVERY_SIX_HOURS));
            });

            services.AddQuartzHostedService();

            return services;
        }
    }
}
