namespace CompanyStatistics.Domain.Abstraction.Services
{
    public interface IGetInfoFromDbService
    {
        static HashSet<string> CompanyIds { get; set; }
        static HashSet<string> Industries { get; set; }
        Task SetCompanyIdsAsync();
        Task SetIndustriesAsync();
        void UpdateCompanyIds(IEnumerable<string> newIds);
        void UpdateIndustries(IEnumerable<string> newIndustries);
    }
}
