using CompanyStatistics.Domain.Abstraction.Repositories;
using Microsoft.Extensions.Configuration;

namespace CompanyStatistics.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IConfiguration _configuration;
        private ICompanyRepository _companyRepository;
        private IAccountRepository _accountRepository;

        public UnitOfWork(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ICompanyRepository CompanyRepository
        {
            get
            {
                if (_companyRepository== null)
                {
                    _companyRepository = new CompanyRepository(_configuration);
                }
                return _companyRepository;
            }
        }

        public IAccountRepository AccountRepository
        {
            get
            {
                if (_accountRepository == null)
                {
                    _accountRepository = new AccountRepository(_configuration);
                }
                return _accountRepository;
            }
        }
    }
}
