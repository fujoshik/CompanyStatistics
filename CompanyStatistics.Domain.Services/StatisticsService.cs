using CompanyStatistics.Domain.Abstraction.Repositories;
using CompanyStatistics.Domain.Abstraction.Services;
using CompanyStatistics.Domain.DTOs.Company;

namespace CompanyStatistics.Domain.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StatisticsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CountEmployeesByIndustryAsync(string industry)
        {
            return await _unitOfWork.CompanyRepository.CountEmployeesByIndustryAsync(industry);
        }

        public async Task<List<CompanyResponseDto>> GetTopNCompaniesByEmployeeCountAsync(int n)
        {
            return await _unitOfWork.CompanyRepository.GetTopNCompaniesByEmployeeCountAsync(n);
        }

        public async Task<List<CompanyResponseDto>> GroupCompaniesByCountryAsync(string country)
        {
            return await _unitOfWork.CompanyRepository.GroupCompaniesByCountryAsync(country);
        }
    }
}
