using CompanyStatistics.API.Configuration;
using CompanyStatistics.Domain.Abstraction.Services;
using CompanyStatistics.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace CompanyStatistics.API.Controllers
{
    [Route("api/pdfs")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        private readonly IPdfService _pdfService;
        private readonly IFileService _fileService;

        public PdfController(IPdfService pdfService,
                             IFileService fileService)
        {
            _pdfService = pdfService;
            _fileService = fileService;
        }

        [AuthorizeRoles(Role.Admin, Role.Regular)]
        [HttpGet("get-pdf-info-about-company")]
        public async Task<IActionResult> GetPdfByCompanyName([FromQuery] string companyName)
        {
            var fileName = await _pdfService.GetPdfForByCompanyName(companyName);

            var dataStream = _fileService.ReturnFileAsStream(fileName);

            return File(dataStream, "application/octet-stream", Path.GetFileName(fileName));
        }
    }
}
