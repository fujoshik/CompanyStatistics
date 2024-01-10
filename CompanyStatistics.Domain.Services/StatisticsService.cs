using CompanyStatistics.Domain.Abstraction.Repositories;
using CompanyStatistics.Domain.Abstraction.Services;
using CompanyStatistics.Domain.DTOs.Company;
using Hangfire;

namespace CompanyStatistics.Domain.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReadDataService _readDataService;

        public StatisticsService(IUnitOfWork unitOfWork,
                                 IReadDataService readDataService)
        {
            _unitOfWork = unitOfWork;
            _readDataService = readDataService;
        }

        public async Task<int> CountEmployeesByIndustryAsync(string industry)
        {
            await _readDataService.ReadFilesAsync();

            return await _unitOfWork.CompanyRepository.CountEmployeesByIndustryAsync(industry);
        }

        public async Task<List<CompanyResponseDto>> GetTopNCompaniesByEmployeeCountAndDateAsync(int n, DateTime? date = null)
        {
            await _readDataService.ReadFilesAsync();

            return await _unitOfWork.CompanyRepository.GetTopNCompaniesByEmployeeCountAndDateAsync(n, date);
        }

        public async Task<List<CompanyResponseDto>> GroupCompaniesByCountryAndIndustryAsync(string country, string industry)
        {
            await _readDataService.ReadFilesAsync();

            return await _unitOfWork.CompanyRepository.GroupCompaniesByCountryAndIndustryAsync(country, industry);
        }
    }
}
