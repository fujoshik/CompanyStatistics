using CompanyStatistics.Domain.Abstraction.Services;
using Microsoft.AspNetCore.Mvc;

namespace CompanyStatistics.API.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IReadDataService _readDataService;

        public CompanyController(IReadDataService readDataService)
        {
            _readDataService = readDataService;
        }

        [HttpGet("read-data")]
        public async Task<IActionResult> ReadData()
        {
            await _readDataService.ReadFilesAsync();

            return Ok();
        }
    }
}
