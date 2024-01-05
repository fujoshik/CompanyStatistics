using CompanyStatistics.Domain.Abstraction.Services;
using CompanyStatistics.Domain.DTOs.Company;
using Microsoft.AspNetCore.Mvc;

namespace CompanyStatistics.API.Controllers
{
    [Route("api/statistics")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        [HttpGet("employee-count-by-industry")]
        public async Task<ActionResult<int>> CountEmployeesByIndustryAsync(string industry)
        {
            var count = await _statisticsService.CountEmployeesByIndustryAsync(industry);

            return Ok(count);
        }

        [HttpGet("get-top-n-companies-by-employee-count")]
        public async Task<ActionResult<CompanyResponseDto>> GetTopNCompaniesByEmployeeCountAsync(int n)
        {
            var companies = await _statisticsService.GetTopNCompaniesByEmployeeCountAsync(n);

            return Ok(companies);
        }

        [HttpGet("group-companies-by-country")]
        public async Task<ActionResult<CompanyResponseDto>> GroupCompaniesByCountryAsync(string country)
        {
            var companies = await _statisticsService.GroupCompaniesByCountryAsync(country);

            return Ok(companies);
        }
    }
}
