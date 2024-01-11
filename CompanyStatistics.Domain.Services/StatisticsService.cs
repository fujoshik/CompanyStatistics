using CompanyStatistics.Domain.Abstraction.Repositories;
using CompanyStatistics.Domain.Abstraction.Services;
using CompanyStatistics.Domain.DTOs.Company;

namespace CompanyStatistics.Domain.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReadFilesService _readDataService;

        public StatisticsService(IUnitOfWork unitOfWork,
                                 IReadFilesService readDataService)
        {
            _unitOfWork = unitOfWork;
            _readDataService = readDataService;
        }

        public async Task<int> CountEmployeesByIndustryAsync(string industry)
        {
            return await _unitOfWork.CompanyRepository.CountEmployeesByIndustryAsync(industry);
        }

        public async Task<List<CompanyResponseDto>> GetTopNCompaniesByEmployeeCountAndDateAsync(int n, DateTime? date = null)
        {
            return await _unitOfWork.CompanyRepository.GetTopNCompaniesByEmployeeCountAndDateAsync(n, date);
        }

        public async Task<List<CompanyResponseDto>> GroupCompaniesByCountryAndIndustryAsync(string country, string industry)
        {
            return await _unitOfWork.CompanyRepository.GroupCompaniesByCountryAndIndustryAsync(country, industry);
        }
    }
}
