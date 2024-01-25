using CompanyStatistics.Domain.DTOs.Company;
using CompanyStatistics.Domain.DTOs.Organization;

namespace CompanyStatistics.Domain.Abstraction.Services
{
    public interface ICompanyIndustriesService
    {
        Task SaveCompanyIndustriesAsync(List<OrganizationDto> organizations);
        Task CreateCompanyIndustryAsync(CompanyRequestDto company);
    }
}
