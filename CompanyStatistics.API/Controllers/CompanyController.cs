﻿using CompanyStatistics.API.Configuration;
using CompanyStatistics.Domain.Abstraction.Services;
using CompanyStatistics.Domain.DTOs.Company;
using CompanyStatistics.Domain.Enums;
using CompanyStatistics.Domain.Pagination;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CompanyStatistics.API.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IReadFilesService _readDataService;
        private readonly ICompanyService _companyService;
        private readonly ILogger<CompanyController> _logger;

        public CompanyController(IReadFilesService readDataService,
                                 ICompanyService companyService,
                                 ILogger<CompanyController> logger)
        {
            _readDataService = readDataService;
            _companyService = companyService;
            _logger = logger;
        }

        [HttpGet("read-data")]
        public async Task<IActionResult> ReadData()
        {
            Stopwatch watch = Stopwatch.StartNew();
            watch.Start();

            await _readDataService.ReadFilesAsync();

            watch.Stop();
            _logger.LogInformation(watch.Elapsed.Seconds.ToString());

            return Ok();
        }

        [HttpPost]
        [AuthorizeRoles(Role.Admin, Role.Regular)]
        public async Task<IActionResult> CreateAsync([FromBody] CompanyCreateDto company)
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
        public async Task<IActionResult> UpdateAsync([FromRoute] string id, [FromBody] CompanyCreateDto company)
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
