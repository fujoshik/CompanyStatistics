using CompanyStatistics.Domain.DTOs.Company;
using CompanyStatistics.Domain.DTOs.Industry;

namespace CompanyStatistics.Domain.Abstraction.Services
{
    public interface IIndustryService
    {
        Task CreateIndustryIfNotExistAsync(CompanyRequestDto companyRequest);
        Task SeparateIndustriesAndSaveThemAsync(List<IndustryRequestDto> industries);
    }
}
