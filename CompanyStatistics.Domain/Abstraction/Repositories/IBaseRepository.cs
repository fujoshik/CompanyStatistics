using CompanyStatistics.Domain.Pagination;

namespace CompanyStatistics.Domain.Abstraction.Repositories
{
    public interface IBaseRepository
    {
        Task<TOutput> InsertAsync<TInput, TOutput>(TInput dto)
            where TOutput : new();
        Task<TOutput> GetByIdAsync<TOutput>(string id)
            where TOutput : new();
        Task<PaginatedResult<TOutput>> GetPageAsync<TOutput>(int pageNumber, int pageSize)
            where TOutput : new();
        Task<TOutput> UpdateAsync<TInput, TOutput>(string id, TInput dto)
            where TOutput : new();
        Task DeleteAsync(string id);
    }
}
