using CompanyStatistics.Domain.DTOs.Company;

namespace CompanyStatistics.UI.Factories.Abstraction
{
    public interface ICompanyFactory
    {
        CompanyCreateDto CreateCompanyCreateDto(string name, string website,
            string description, string country, int founded, int numberOfEmployee, string industries);
    }
}
