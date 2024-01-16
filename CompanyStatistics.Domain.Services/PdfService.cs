using CompanyStatistics.Domain.Abstraction.Services;
using CompanyStatistics.Domain.DTOs.Company;
using CompanyStatistics.Domain.Paths;
using Microsoft.Extensions.Options;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace CompanyStatistics.Domain.Services
{
    public class PdfService : IPdfService
    {
        private readonly ICompanyService _companyService;
        private readonly string _pdfFilesPath;

        public PdfService(ICompanyService companyService,
                          IOptions<FilesFolderPath> filesFolderPathOptions)
        {
            _companyService = companyService;
            _pdfFilesPath = filesFolderPathOptions.Value.PdfFilesPath;
        }

        public async Task<string> GetPdfForByCompanyName(string companyName)
        {
            var company = await _companyService.GetCompanyByNameAsync(companyName);

            return BuildPdf(company);
        }

        private string BuildPdf(CompanyResponseDto company)
        {
            string fileName = string.Format($@"{_pdfFilesPath}{DateTime.UtcNow.Ticks}.pdf");
            QuestPDF.Settings.License = LicenseType.Community;

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Header().AlignCenter().Text($"Information about {company.Name}").FontSize(30);
                    page.Content()
                    .AlignCenter()
                    .PaddingVertical(1, Unit.Centimetre)
                    .Column(x =>
                    {
                        x.Spacing(20);
                        x.Item().Text($"Name: {company.Name}");
                        x.Item().Text($"Website: {company.Website}");
                        x.Item().Text($"Country: {company.Country}");
                        x.Item().Text($"Description: {company.Description}");
                        x.Item().Text($"Founded in year {company.Founded}");
                        x.Item().Text($"Industry: {company.Industry}");
                        x.Item().Text($"Number of employees: {company.NumberOfEmployees}");
                    });
                });
            }).GeneratePdf(fileName);

            return fileName;
        }
    }
}
