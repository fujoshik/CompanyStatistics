namespace CompanyStatistics.Domain.Abstraction.Repositories
{
    public interface IUnitOfWork
    {
        ICompanyRepository CompanyRepository { get; }
        IAccountRepository AccountRepository { get; }
        IUserRepository UserRepository { get; }
        IIndustryRepository IndustryRepository { get; }
        ICompanyIndustriesRepository CompanyIndustriesRepository { get; }
    }
}
