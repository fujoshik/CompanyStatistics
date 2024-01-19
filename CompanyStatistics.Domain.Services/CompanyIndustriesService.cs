using CompanyStatistics.Domain.Abstraction.Repositories;
using CompanyStatistics.Domain.Abstraction.Services;
using CompanyStatistics.Domain.DTOs.CompanyIndustry;
using CompanyStatistics.Domain.DTOs.Organization;

namespace CompanyStatistics.Domain.Services
{
    public class CompanyIndustriesService : ICompanyIndustriesService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyIndustriesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                    result.Add(new CompanyIndustryRequestDto
                    {
                        CompanyId = organization.OrganizationId,
                        IndustryName = entry,
                        IsDeleted = 0
                    });
                }
            }

            await _unitOfWork.CompanyIndustriesRepository.BulkInsertAsync(result);
        }
    }
}
