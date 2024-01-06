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
        public async Task<ActionResult<int>> CountEmployeesByIndustryAsync([FromQuery] string industry)
        {
            var count = await _statisticsService.CountEmployeesByIndustryAsync(industry);

            return Ok(count);
        }

        [HttpGet("get-top-n-companies-by-employee-count")]
        public async Task<ActionResult<CompanyResponseDto>> GetTopNCompaniesByEmployeeCountAsync([FromQuery] int n)
        {
            var companies = await _statisticsService.GetTopNCompaniesByEmployeeCountAsync(n);

            return Ok(companies);
        }

        [HttpGet("group-companies-by-country-and-industry")]
        public async Task<ActionResult<CompanyResponseDto>> GroupCompaniesByCountryAndIndustryAsync([FromQuery] string country = null, [FromQuery] string industry = null)
        {
            var companies = await _statisticsService.GroupCompaniesByCountryAndIndustryAsync(country, industry);

            return Ok(companies);
        }
    }
}
