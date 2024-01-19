using CompanyStatistics.Domain.DTOs.Industry;

namespace CompanyStatistics.Domain.Abstraction.Repositories
{
    public interface IIndustryRepository : IBaseRepository
    {
        Task BulkInsertAsync(List<IndustryRequestDto> industries);
        Task<HashSet<string>> GetAllIndustriesAsync();
    }
}
