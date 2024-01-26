using CompanyStatistics.Domain.Abstraction.Factories;
using CompanyStatistics.Domain.DTOs.CompanyIndustry;

namespace CompanyStatistics.Domain.Factories
{
    public class CompanyIndustryFactory : ICompanyIndustryFactory
    {
        public CompanyIndustryRequestDto CreateCompanyIndustryRequestDto(string companyId, string industryName)
        {
            return new CompanyIndustryRequestDto
            {
                CompanyId = companyId,
                IndustryName = industryName,
                IsDeleted = 0
            };
        }
    }
}
