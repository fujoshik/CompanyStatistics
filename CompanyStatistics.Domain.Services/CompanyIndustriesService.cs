using CompanyStatistics.Domain.Abstraction.Factories;
using CompanyStatistics.Domain.Abstraction.Repositories;
using CompanyStatistics.Domain.Abstraction.Services;
using CompanyStatistics.Domain.DTOs.Company;
using CompanyStatistics.Domain.DTOs.CompanyIndustry;
using CompanyStatistics.Domain.DTOs.Organization;

namespace CompanyStatistics.Domain.Services
{
    public class CompanyIndustriesService : ICompanyIndustriesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICompanyIndustryFactory _companyIndustryFactory;

        public CompanyIndustriesService(IUnitOfWork unitOfWork,
                                        ICompanyIndustryFactory companyIndustryFactory)
        {
            _unitOfWork = unitOfWork;
            _companyIndustryFactory = companyIndustryFactory;
        }

        public async Task SaveCompanyIndustriesAsync(List<OrganizationDto> organizations)
        {
            var result = new List<CompanyIndustryRequestDto>();

            foreach (var organization in organizations)
            {
                var entries = organization.Industry
                    .Split(new char[] { '/', }, StringSplitOptions.TrimEntries);

                foreach (var entry in entries)
                {
                    var companyIndustry = _companyIndustryFactory.CreateCompanyIndustryRequestDto(
                        organization.OrganizationId, entry);

                    result.Add(companyIndustry);
                }
            }

            await _unitOfWork.CompanyIndustriesRepository.BulkInsertAsync(result);
        }

        public async Task CreateCompanyIndustryAsync(CompanyRequestDto company)
        {
            var result = new List<CompanyIndustryRequestDto>();

            foreach (var industry in company.Industries)
            {
                var companyIndustry = _companyIndustryFactory.CreateCompanyIndustryRequestDto(company.Id, industry.Name);

                result.Add(companyIndustry);
            }

            await _unitOfWork.CompanyIndustriesRepository.BulkInsertAsync(result);
        }
    }
}
