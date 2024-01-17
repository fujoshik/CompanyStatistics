using AutoMapper;
using CompanyStatistics.Domain.Abstraction.Repositories;
using CompanyStatistics.Domain.Abstraction.Services;
using CompanyStatistics.Domain.DTOs.Company;
using CompanyStatistics.Domain.DTOs.File;
using CompanyStatistics.Domain.DTOs.Organization;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace CompanyStatistics.Domain.Services
{
    public class ReadDataService : IReadDataService
    {
        private readonly IMapper _mapper;
        private readonly IMongoDbService _mongoDbService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGetInfoFromDbService _getInfoFromDbService;
        private HashSet<string> _companyIds;

        public ReadDataService(IMapper mapper,
                               IUnitOfWork unitOfWork,
                               IMongoDbService mongoDbService,
                               IGetInfoFromDbService getInfoFromDbService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _mongoDbService = mongoDbService;
            _getInfoFromDbService = getInfoFromDbService;
        }

        public async Task ReadCsvFileAsync(string fileName)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                Escape = '\\'
            };

            using var streamReader = File.OpenText(fileName);

            using var csv = new CsvReader(streamReader, config);

            var records = csv.GetRecords<OrganizationDto>().ToList();

            records = FilterExisitingRecords(records);

            var companies = _mapper.ProjectTo<CompanyRequestDto>(records.AsQueryable()).ToList();

            UpdateCompanyIds(companies);

            await _unitOfWork.CompanyRepository.BulkInsertAsync(companies);
        }

        private void UpdateCompanyIds(List<CompanyRequestDto> companies)
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

        private async Task SaveReadDocumentNameAndIndex(string name, int index)
        {
            if (index == 1)
            {
                await _mongoDbService.CreateFileAsync(new FileDto() { FileName = name, Index = index });
            }
            else
            {
                var file = await _mongoDbService.GetFileByNameAsync(name);

                if (file == null)
                {
                    await _mongoDbService.CreateFileAsync(new FileDto() { FileName = name, Index = index });
                }
                else
                {
                    await _mongoDbService.UpdateFileIndexAsync(name, index);
                }
            }
        }
    }
}
