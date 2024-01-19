using AutoMapper;
using CompanyStatistics.Domain.Abstraction.Repositories;
using CompanyStatistics.Domain.Abstraction.Services;
using CompanyStatistics.Domain.DTOs.Company;
using CompanyStatistics.Domain.DTOs.Industry;

namespace CompanyStatistics.Domain.Services
{
    public class IndustryService : IIndustryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGetInfoFromDbService _getInfoFromDbService;
        private HashSet<string> _industries;

        public IndustryService(IUnitOfWork unitOfWork,
                               IGetInfoFromDbService getInfoFromDbService)
        {
            _unitOfWork = unitOfWork;
            _getInfoFromDbService = getInfoFromDbService;     
        }

        public async Task SeparateIndustriesAndSaveThemAsync(List<IndustryRequestDto> industries)
        {
            var result = new List<IndustryRequestDto>();
            _industries = GetInfoFromDbService.Industries;

            foreach (var item in industries)
            {
                var entries = item.Name
                    .Split(new char[] { '/', }, StringSplitOptions.TrimEntries);

                result.AddRange(entries.Select(x => new IndustryRequestDto { Name = x }));
            }

            result = result.DistinctBy(x => x.Name).ToList(); 

            if (_industries.Count > 0)
            {
                _getInfoFromDbService.UpdateIndustries(result.Select(x => x.Name));

                _industries = GetInfoFromDbService.Industries;
            }

            var newIndustries = result
                .Where(x => !_industries.Contains(x.Name))
                .ToList();

            await _unitOfWork.IndustryRepository.BulkInsertAsync(newIndustries);

            _getInfoFromDbService.UpdateIndustries(result.Select(x => x.Name));
        }

        public async Task CreateIndustryIfNotExist(CompanyRequestDto companyRequest)
        {
            var industries = new List<IndustryRequestDto>
            {
                new IndustryRequestDto { Name = companyRequest.Industry }
            };

            await SeparateIndustriesAndSaveThemAsync(industries);
        }
    }
}
