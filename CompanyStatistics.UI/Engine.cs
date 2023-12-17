using CompanyStatistics.Domain.DTOs.Organization;
using OfficeOpenXml;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace CompanyStatistics.UI
{
    public class Engine
    {
        private readonly HttpClient _httpClient;
        private readonly string _url = "https://localhost:7073/api/data/";
        public Engine()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(_url)
            };
        }

        public async Task ReadFilesAsync(string folderPath)
        {
            var files = Directory.GetFiles(folderPath);

            foreach (var file in files)
            {
                await RunAsync(file);
            }
        }

        public async Task RunAsync(string path)
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
                    organizationList.Add(new OrganizationDto()
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
                    });
                }

                await SendToApiAsync(organizationList);
            }
        }

        private async Task SendToApiAsync(List<OrganizationDto> organizations)
        {
            var response = await _httpClient.PostAsJsonAsync(_url + "read-data", organizations);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException((int)response.StatusCode + " " + response.ReasonPhrase);
            }
        }
    }
}
