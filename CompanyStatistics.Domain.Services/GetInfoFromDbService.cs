using CompanyStatistics.Domain.Abstraction.Repositories;
using CompanyStatistics.Domain.Abstraction.Services;

namespace CompanyStatistics.Domain.Services
{
    public class GetInfoFromDbService : IGetInfoFromDbService
    {
        private readonly IUnitOfWork _unitOfWork;
        private static HashSet<string> _companyIds;
        private static HashSet<string> _industries;
        public static HashSet<string> CompanyIds
        {
            get
            {
                return _companyIds;
            }
            set
            {
                _companyIds = value;
            }
        }

        public static HashSet<string> Industries
        {
            get
            {
                return _industries;
            }
            set
            {
                _industries = value;
            }
        }

        public GetInfoFromDbService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task SetCompanyIdsAsync()
        {
            if (_companyIds == null)
            {
                _companyIds = await _unitOfWork.CompanyRepository.GetAllCompanyIdsAsync();
            }
        }

        public void UpdateCompanyIds(IEnumerable<string> newIds)
        {
            _companyIds.UnionWith(newIds);
        }

        public async Task SetIndustriesAsync()
        {
            if (_industries == null)
            {
                _industries = await _unitOfWork.IndustryRepository.GetAllIndustriesAsync();
            }
        }

        public void UpdateIndustries(IEnumerable<string> newIndustries)
        {
            _industries.UnionWith(newIndustries);
        }
    }
}
