using AutoMapper;
using CompanyStatistics.Domain.Abstraction.Services;
using CompanyStatistics.Domain.DTOs.Company;
using CompanyStatistics.Domain.DTOs.File;
using CompanyStatistics.Domain.DTOs.Organization;
using CompanyStatistics.Domain.Paths;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Options;
using OfficeOpenXml;
using System.Globalization;
using System.Text.RegularExpressions;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace CompanyStatistics.Domain.Services
{
    public class ReadDataService : IReadDataService
    {
        private readonly string _folderPath;
        private readonly IMapper _mapper;
        private readonly ICompanyService _companyService;
        private readonly IMongoDbService _mongoDbService;

        public ReadDataService(IOptions<FilesFolderPath> folderPath,
                               IMapper mapper,
                               ICompanyService companyService,
                               IMongoDbService mongoDbService)
        {
            _folderPath = folderPath.Value.Path;
            _mapper = mapper;
            _companyService = companyService;
            _mongoDbService = mongoDbService;
        }

        private void MoveFile(string file)
        {
            if (File.Exists(file))
            {
                File.Move(file, _folderPath + $"\\ReadFiles\\{Path.GetFileName(file)}");
            }
        }

        public async Task ReadFilesAsync()
        {
            var files = Directory.GetFiles(_folderPath);

            foreach (var file in files)
            {
                if (file.EndsWith(".csv"))
                {
                    await ReadCsvFileAsync(file);
                }
                else
                {
                    await ReadXlsxFileAsync(file);
                }

                MoveFile(file);
            }
        }

        public async Task ReadCsvFileAsync(string fileName)
        {
            List<OrganizationDto> organizations = new List<OrganizationDto>();

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                Mode = CsvMode.RFC4180
            };

            //string fileContent = File.ReadAllText(fileName);
            //fileContent = fileContent.Replace("'", "''").Replace("\"", "");
            using var streamReader = File.OpenText(fileName);
            //using var streamReader = new StringReader(fileContent);

            using var csv = new CsvReader(streamReader, config);

            var records = csv.GetRecords<OrganizationDto>();

            foreach (var record in records)
            {
                await InsertCompanyAndSaveReadFile(fileName, record);
            }
        }

        public async Task ReadXlsxFileAsync(string path)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            List<OrganizationDto> organizationList = new List<OrganizationDto>();

            using (ExcelPackage package = new ExcelPackage(new FileInfo(path)))
            {
                int startIndex = 1;

                var sheet = package.Workbook.Worksheets["Лист1"];
                var result = sheet.Cells[1, 1].Value.ToString().Split(",");

                var file = await _mongoDbService.GetFileByNameAsync(path);

                if (file != null)
                {
                    startIndex = file.Index + 1;
                }

                for (int i = startIndex + 1; i <= sheet.Dimension.Rows; i++)
                {
                    var row = Regex.Split(sheet.Cells[i, 1].Value.ToString().Replace("'", "''").Replace("\"", ""), @",(?!\s)");
                    var organization = new OrganizationDto()
                    {
                        Index = int.Parse(row[0]),
                        OrganizationId = row[1],
                        Name = row[2],
                        Website = row[3],
                        Country = row[4],
                        Description = row[5],
                        Founded = row[6],
                        Industry = row[7],
                        NumberOfEmployees = int.Parse(row[8])
                    };
                    organizationList.Add(organization);

                    await InsertCompanyAndSaveReadFile(path, organization);
                }
            }
        }

        private async Task InsertCompanyAndSaveReadFile(string path, OrganizationDto organization)
        {
            var companyRequest = _mapper.Map<CompanyRequestDto>(organization);

            await _companyService.CreateAsync(companyRequest);

            await SaveReadDocumentNameAndIndex(path, organization.Index);
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
