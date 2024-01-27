using CompanyStatistics.Domain.DTOs.Company;
using CompanyStatistics.Domain.DTOs.Industry;
using CompanyStatistics.UI.Factories.Abstraction;

namespace CompanyStatistics.UI.Factories
{
    public class CompanyFactory : ICompanyFactory
    {
        private readonly IIndustryFactory _industryFactory;

        public CompanyFactory()
        {
            _industryFactory = new IndustryFactory();
        }

        public CompanyCreateDto CreateCompanyCreateDto(string name, string website, 
            string description, string country, int founded, int numberOfEmployee, string industries)
        {
            var industriesList = CreateIndustriesList(industries);

            return new CompanyCreateDto
            {
                Name = name,
                Description = description,
                Country = country,
                Founded = founded,
                Website = website,
                Industries = industriesList,
                NumberOfEmployees = numberOfEmployee,
            };
        }

        private List<IndustryRequestDto> CreateIndustriesList(string industries)
        {
            var newIndustries = industries.Split(", ");
            var industriesList = new List<IndustryRequestDto>();

            foreach (var item in newIndustries)
            {
                var industryRequest = _industryFactory.CreateIndustryRequestDto(item);

                industriesList.Add(industryRequest);
            }

            return industriesList;
        }
    }
}
