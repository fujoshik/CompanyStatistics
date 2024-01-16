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

        public ReadDataService(IMapper mapper,
                               IUnitOfWork unitOfWork,
                               IMongoDbService mongoDbService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _mongoDbService = mongoDbService;
        }

        public async Task ReadCsvFileAsync(string fileName)
        {
            int startIndex = 1;

            var file = await _mongoDbService.GetFileByNameAsync(fileName);

            if (file != null)
            {
                startIndex = file.Index;
            }

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                Escape = '\\'
            };

            using var streamReader = File.OpenText(fileName);

            using var csv = new CsvReader(streamReader, config);

            var records = csv.GetRecords<OrganizationDto>().AsQueryable();
            var companies = _mapper.ProjectTo<CompanyRequestDto>(records).ToList();

            await _unitOfWork.CompanyRepository.BulkInsertAsync(companies);

            //await SaveReadDocumentNameAndIndex(fileName, companies.Count);
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
