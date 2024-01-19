using CompanyStatistics.Domain.DTOs.Organization;

namespace CompanyStatistics.Domain.Abstraction.Services
{
    public interface ICompanyIndustriesService
    {
        Task SaveCompanyIndustriesAsync(List<OrganizationDto> organizations);
    }
}
