using AutoMapper;
using CompanyStatistics.Domain.Abstraction.Repositories;
using Microsoft.Extensions.Configuration;

namespace CompanyStatistics.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private ICompanyRepository _companyRepository;
        private IAccountRepository _accountRepository;
        private IUserRepository _userRepository;
        private IIndustryRepository _industryRepository;
        private ICompanyIndustriesRepository _companyIndustriesRepository;

        public UnitOfWork(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public ICompanyRepository CompanyRepository
        {
            get
            {
                if (_companyRepository== null)
                {
                    _companyRepository = new CompanyRepository(_configuration, _mapper);
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

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_configuration);
                }
                return _userRepository;
            }
        }

        public IIndustryRepository IndustryRepository
        {
            get
            {
                if (_industryRepository == null)
                {
                    _industryRepository = new IndustryRepository(_configuration);
                }
                return _industryRepository;
            }
        }

        public ICompanyIndustriesRepository CompanyIndustriesRepository
        {
            get
            {
                if (_companyIndustriesRepository == null)
                {
                    _companyIndustriesRepository = new CompanyIndustriesRepository(_configuration);
                }
                return _companyIndustriesRepository;
            }
        }
    }
}
