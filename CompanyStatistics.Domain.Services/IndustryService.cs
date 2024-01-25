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
            var result = SeparateIndustries(industries);

            var newIndustries = FilterExistingIndustries(result);

            await SaveIndustriesAsync(newIndustries);
        }

        public async Task CreateIndustryIfNotExistAsync(CompanyRequestDto companyRequest)
        {
            var filtered = FilterExistingIndustries(companyRequest.Industries);

            if (filtered.Count > 0)
            {
                await SaveIndustriesAsync(filtered);
            }      
        }

        private List<IndustryRequestDto> SeparateIndustries(List<IndustryRequestDto> industries)
        {
            var result = new List<IndustryRequestDto>();

            foreach (var item in industries)
            {
                var entries = item.Name
                    .Split(new char[] { '/', }, StringSplitOptions.TrimEntries);

                result.AddRange(entries.Select(x => new IndustryRequestDto { Name = x }));
            }

            return result.DistinctBy(x => x.Name).ToList();
        }

        private List<IndustryRequestDto> FilterExistingIndustries(List<IndustryRequestDto> result)
        {
            _industries = GetInfoFromDbService.Industries;

            if (_industries.Count > 0)
            {
                _getInfoFromDbService.UpdateIndustries(result.Select(x => x.Name));

                _industries = GetInfoFromDbService.Industries;
            }

            return result
                .Where(x => !_industries.Contains(x.Name))
                .ToList();
        }

        private async Task SaveIndustriesAsync(List<IndustryRequestDto> newIndustries)
        {
            await _unitOfWork.IndustryRepository.BulkInsertAsync(newIndustries);

            _getInfoFromDbService.UpdateIndustries(newIndustries.Select(x => x.Name));
        }
    }
}
