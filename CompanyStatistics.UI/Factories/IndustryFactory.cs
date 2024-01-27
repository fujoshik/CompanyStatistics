using CompanyStatistics.Domain.DTOs.Industry;
using CompanyStatistics.UI.Factories.Abstraction;

namespace CompanyStatistics.UI.Factories
{
    public class IndustryFactory : IIndustryFactory
    {
        public IndustryRequestDto CreateIndustryRequestDto(string name)
        {
            return new IndustryRequestDto
            {
                Name = name
            };
        }
    }
}
