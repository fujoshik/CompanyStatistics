using CompanyStatistics.Domain.Abstraction.Services;
using Quartz;

namespace CompanyStatistics.Domain.Jobs
{
    public class GetCompanyIdsFromDbJob : IJob
    {
        private readonly IGetInfoFromDbService _getInfoFromDbService;

        public GetCompanyIdsFromDbJob(IGetInfoFromDbService getInfoFromDb)
        {
            _getInfoFromDbService = getInfoFromDb;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            if (IGetInfoFromDbService.CompanyIds == null)
            {
                await _getInfoFromDbService.SetCompanyIdsAsync();
            }
        }      
    }
}
