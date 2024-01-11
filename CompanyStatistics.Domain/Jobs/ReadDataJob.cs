using CompanyStatistics.Domain.Abstraction.Services;
using Quartz;

namespace CompanyStatistics.Domain.Jobs
{
    public class ReadDataJob : IJob
    {
        private readonly IReadDataService _readDataService;

        public ReadDataJob(IReadDataService readDataService)
        {
            _readDataService = readDataService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _readDataService.ReadFilesAsync();
        }
    }
}
