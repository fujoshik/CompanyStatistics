namespace CompanyStatistics.Domain.Abstraction.Services
{
    public interface IGetInfoFromDbService
    {
        static HashSet<string> CompanyIds { get; set; }
        Task SetCompanyIdsAsync();
        void UpdateCompanyIds(IEnumerable<string> newIds);
    }
}
