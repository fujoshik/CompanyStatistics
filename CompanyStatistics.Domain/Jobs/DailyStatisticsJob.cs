using CompanyStatistics.Domain.Abstraction.Services;
using Quartz;

namespace CompanyStatistics.Domain.Jobs
{
    public class DailyStatisticsJob : IJob
    {
        private readonly IStatisticsService _statisticsService;
        private readonly IFileService _fileService;

        public DailyStatisticsJob(IFileService fileService,
                                  IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
            _fileService = fileService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var yesterdayDate = DateTime.Now.AddDays(-1);

            var companiesCount = await _statisticsService.GetCompaniesCountByDateAsync(yesterdayDate);

            _fileService.WriteAJsonFileWithStatistics(companiesCount, yesterdayDate.Date.ToString("d"));
        }
    }
}
