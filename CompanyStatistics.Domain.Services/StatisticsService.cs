using CompanyStatistics.Domain.Abstraction.Repositories;
using CompanyStatistics.Domain.Abstraction.Services;
using CompanyStatistics.Domain.DTOs.Company;

namespace CompanyStatistics.Domain.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICompanyService _companyService;

        public StatisticsService(IUnitOfWork unitOfWork,
                                 ICompanyService companyService)
        {
            _unitOfWork = unitOfWork;
            _companyService = companyService;
        }

        public async Task<int> CountEmployeesByIndustryAsync(string industry)
        {
            return await _unitOfWork.CompanyRepository.CountEmployeesByIndustryAsync(industry);
        }

        public async Task<int> GetCompaniesCountByDateAsync(DateTime date)
        {
            return await _unitOfWork.CompanyRepository.GetCompaniesCountByDateAsync(date);
        }

        public async Task<List<CompanyResponseDto>> GetTopNCompaniesByEmployeeCountAndDateAsync(int n, DateTime? date = null)
        {
            var companies = await _unitOfWork.CompanyRepository.GetTopNCompaniesByEmployeeCountAndDateAsync(n, date);

            return await AssignIndustriesToCompaniesAsync(companies);
        }

        public async Task<List<CompanyResponseDto>> GroupCompaniesByCountryAndIndustryAsync(string country, string industry)
        {
            var companies = await _unitOfWork.CompanyRepository.GroupCompaniesByCountryAndIndustryAsync(country, industry);

            return await AssignIndustriesToCompaniesAsync(companies);
        }

        private async Task<List<CompanyResponseDto>> AssignIndustriesToCompaniesAsync(List<CompanyWithoutIndustryDto> companies)
        {
            var result = new List<CompanyResponseDto>();

            foreach (var company in companies)
            {
                var companyResponse = await _companyService.AssignIndustriesAsync(company);

                result.Add(companyResponse);
            }

            return result;
        }
    }
}
