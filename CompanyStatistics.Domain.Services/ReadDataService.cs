using AutoMapper;
using CompanyStatistics.Domain.Abstraction.Services;
using CompanyStatistics.Domain.DTOs.Company;
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

        public ReadDataService(IOptions<FilesFolderPath> folderPath,
                               IMapper mapper,
                               ICompanyService companyService)
        {
            _folderPath = folderPath.Value.Path;
            _mapper = mapper;
            _companyService = companyService;
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
                await ReadFileAsync(file);
            }
        }

        public async Task ReadCsvFileAsync(string fileName)
        {
            List<OrganizationDto> records = new List<OrganizationDto>();

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                HasHeaderRecord = true
            };

            using (var reader = new StreamReader(_folderPath + fileName))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Context.RegisterClassMap<OrganizationMap>();

                csv.ReadHeader();
                while (csv.Read())
                {
                    var record = csv.GetRecord<OrganizationDto>();
                    records.Add(record);
                }

                foreach (var record in records)
                    Console.WriteLine(record);
            }
        }

        public async Task ReadFileAsync(string path)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            List<OrganizationDto> organizationList = new List<OrganizationDto>();

            using (ExcelPackage package = new ExcelPackage(new FileInfo(path)))
            {
                var sheet = package.Workbook.Worksheets["Лист1"];
                var result = sheet.Cells[1, 1].Value.ToString().Split(",");

                for (int i = 2; i < sheet.Dimension.Rows; i++)
                {
                    var row = Regex.Split(sheet.Cells[i, 1].Value.ToString().Replace("\"", ""), @",(?!\s)");
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

                    var companyRequest = _mapper.Map<CompanyRequestDto>(organization);

                    await _companyService.CreateAsync(companyRequest);
                }
            }
        }
    }
}
