using CompanyStatistics.Domain.Abstraction.Services;
using Quartz;

namespace CompanyStatistics.Domain.Jobs
{
    public class ReadDataJob : IJob
    {
        private readonly IReadFilesService _readDataService;

        public ReadDataJob(IReadFilesService readDataService)
        {
            _readDataService = readDataService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _readDataService.ReadFilesAsync();
        }
    }
}
