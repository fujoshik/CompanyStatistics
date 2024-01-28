namespace CompanyStatistics.Domain.Abstraction.Repositories
{
    public interface IUserRepository : IBaseRepository
    {
        Task DeleteUserAsync(string id);
    }
}
