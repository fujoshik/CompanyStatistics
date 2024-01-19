using CompanyStatistics.Domain.Abstraction.Services;
using Quartz;

namespace CompanyStatistics.Domain.Jobs
{
    public class GetInfoFromDbJob : IJob
    {
        private readonly IGetInfoFromDbService _getInfoFromDbService;

        public GetInfoFromDbJob(IGetInfoFromDbService getInfoFromDb)
        {
            _getInfoFromDbService = getInfoFromDb;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            if (IGetInfoFromDbService.CompanyIds == null)
            {
                await _getInfoFromDbService.SetCompanyIdsAsync();
                await _getInfoFromDbService.SetIndustriesAsync();
            }
        }      
    }
}
