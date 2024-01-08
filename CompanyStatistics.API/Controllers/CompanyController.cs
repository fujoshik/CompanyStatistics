using CompanyStatistics.API.Configuration;
using CompanyStatistics.Domain.Abstraction.Services;
using CompanyStatistics.Domain.DTOs.Company;
using CompanyStatistics.Domain.Enums;
using CompanyStatistics.Domain.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace CompanyStatistics.API.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IReadDataService _readDataService;
        private readonly ICompanyService _companyService;

        public CompanyController(IReadDataService readDataService,
                                 ICompanyService companyService)
        {
            _readDataService = readDataService;
            _companyService = companyService;
        }

        [HttpGet("read-data")]
        public async Task<IActionResult> ReadData()
        {
            await _readDataService.ReadFilesAsync();

            return Ok();
        }

        [HttpPost]
        [AuthorizeRoles(Role.Admin, Role.Regular)]
        public async Task<IActionResult> CreateAsync([FromBody] CompanyRequestDto company)
        {
            await _companyService.CreateAsync(company);

            return Ok();
        }

        [HttpGet("{id}")]
        [ActionName(nameof(GetByIdAsync))]
        public async Task<ActionResult<CompanyResponseDto>> GetByIdAsync([FromRoute] string id)
        {
            var company = await _companyService.GetByIdAsync(id);

            return Ok(company);
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResult<CompanyResponseDto>>> GetPageAsync(
            [FromQuery] PagingInfo pagingInfo)
        {
            var companies = await _companyService.GetPageAsync(pagingInfo);

            return Ok(companies);
        }

        [HttpPut("{id}")]
        [AuthorizeRoles(Role.Admin, Role.Regular)]
        public async Task<IActionResult> UpdateAsync([FromRoute] string id, [FromBody] CompanyWithoutIdDto company)
        {
            await _companyService.UpdateAsync(id, company);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [AuthorizeRoles(Role.Admin)]
        public async Task<ActionResult> DeleteAsync([FromRoute] string id)
        {
            await _companyService.DeleteAsync(id);

            return NoContent();
        }
    }
}
