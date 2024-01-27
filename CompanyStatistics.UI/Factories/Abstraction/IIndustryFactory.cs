using CompanyStatistics.Domain.DTOs.Industry;

namespace CompanyStatistics.UI.Factories.Abstraction
{
    public interface IIndustryFactory
    {
        IndustryRequestDto CreateIndustryRequestDto(string name);
    }
}
