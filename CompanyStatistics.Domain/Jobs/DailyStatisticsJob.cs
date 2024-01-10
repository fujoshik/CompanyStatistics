using CompanyStatistics.Domain.Abstraction.Repositories;
using CompanyStatistics.Domain.Abstraction.Services;
using CompanyStatistics.Domain.Paths;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Quartz;

namespace CompanyStatistics.Domain.Jobs
{
    public class DailyStatisticsJob : IJob
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStatisticsService _statisticsService;
        private readonly string _directory;

        public DailyStatisticsJob(IUnitOfWork unitOfWork,
                                  IOptions<FilesFolderPath> folderPath,
                                  IStatisticsService statisticsService)
        {
            _unitOfWork = unitOfWork;
            _statisticsService = statisticsService;
            _directory = folderPath.Value.Path;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            //var yesterdayDate = DateTime.UtcNow.AddDays(-1);
            //Console.WriteLine(yesterdayDate.Date.ToString("d"));
            //var topCompaniesByEmployeeCount = await _statisticsService.GetTopNCompaniesByEmployeeCountAndDateAsync(
            //    10, yesterdayDate);

            //var jsonString = JsonConvert.SerializeObject(topCompaniesByEmployeeCount);
            //File.WriteAllText(_directory + "//Statistics//" + yesterdayDate.Date.ToString(), jsonString);
        }
    }
}
