using AutoMapper;
using CompanyStatistics.Domain.Abstraction.Repositories;
using CompanyStatistics.Domain.Abstraction.Services;
using CompanyStatistics.Domain.DTOs.Company;
using CompanyStatistics.Domain.DTOs.Industry;
using CompanyStatistics.Domain.DTOs.Organization;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace CompanyStatistics.Domain.Services
{
    public class ReadDataService : IReadDataService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGetInfoFromDbService _getInfoFromDbService;
        private readonly IIndustryService _industryService;
        private readonly ICompanyIndustriesService _companyIndustriesService;
        private HashSet<string> _companyIds;

        public ReadDataService(IMapper mapper,
                               IUnitOfWork unitOfWork,
                               IIndustryService industryService,
                               IGetInfoFromDbService getInfoFromDbService,
                               ICompanyIndustriesService companyIndustriesService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _industryService = industryService;
            _getInfoFromDbService = getInfoFromDbService;
            _companyIndustriesService = companyIndustriesService;
        }

        public async Task ReadCsvFileAsync(string fileName)
        {
            var records = UseCsvHelperToReadFile(fileName);

            records = FilterExisitingRecords(records);

            var companies = _mapper.ProjectTo<CompanyRequestDto>(records.AsQueryable()).ToList();

            await CheckForMoreIndustriesInOneRow(records);

            UpdateDbInfo(companies);

            await _unitOfWork.CompanyRepository.BulkInsertAsync(companies);
        }

        private List<OrganizationDto> UseCsvHelperToReadFile(string fileName)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                Escape = '\\'
            };

            using var streamReader = File.OpenText(fileName);

            using var csv = new CsvReader(streamReader, config);

            return csv.GetRecords<OrganizationDto>().ToList();
        }

        private async Task CheckForMoreIndustriesInOneRow(List<OrganizationDto> organizations)
        {
            var industries = _mapper.ProjectTo<IndustryRequestDto>(organizations.AsQueryable()).ToList();

            var result = industries.Where(x => x.Name.Contains('/')).ToList();
            
            if (result.Count > 0)
            {
                await _industryService.SeparateIndustriesAndSaveThemAsync(result);
            }

            await _companyIndustriesService.SaveCompanyIndustriesAsync(organizations);
        }

        private void UpdateDbInfo(List<CompanyRequestDto> companies)
        {
            if (companies.Count > 0)
            {
                _getInfoFromDbService.UpdateCompanyIds(companies.Select(x => x.Id));
            }
        }

        private List<OrganizationDto> FilterExisitingRecords(IEnumerable<OrganizationDto> records)
        {
            _companyIds = GetInfoFromDbService.CompanyIds;

            var result = records.Where(x => !_companyIds.Contains(x.OrganizationId)).ToList();

            return result;
        }
    }
}
