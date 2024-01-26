using CompanyStatistics.Domain.DTOs.CompanyIndustry;

namespace CompanyStatistics.Domain.Abstraction.Factories
{
    public interface ICompanyIndustryFactory
    {
        CompanyIndustryRequestDto CreateCompanyIndustryRequestDto(string companyId, string industryName);
    }
}
