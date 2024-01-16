using CompanyStatistics.Domain.Abstraction.Services;
using CompanyStatistics.Domain.DTOs.Company;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CompanyStatistics.API.Controllers
{
    [ApiController]
    [Route("api/statistics")]  
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;
        private readonly IMemoryCache _cache;

        public StatisticsController(IStatisticsService statisticsService,
                                    IMemoryCache memoryCache)
        {
            _statisticsService = statisticsService;
            _cache = memoryCache;
        }

        [HttpGet("employee-count-by-industry")]
        public async Task<ActionResult<int>> CountEmployeesByIndustryAsync([FromQuery] string industry)
        {
            if (!_cache.TryGetValue($"employee-count-by-industry-{industry}", out int count))
            {
                count = await _statisticsService.CountEmployeesByIndustryAsync(industry);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(45))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                    .SetPriority(CacheItemPriority.Normal);

                _cache.Set($"employee-count-by-industry-{industry}", count, cacheEntryOptions);
            }

            return Ok(count);
        }

        [HttpGet("get-top-n-companies-by-employee-count")]
        public async Task<ActionResult<CompanyResponseDto>> GetTopNCompaniesByEmployeeCountAndDateAsync([FromQuery] int n)
        {
            if (!_cache.TryGetValue($"get-top-n-companies-by-employee-count-{n}", out List<CompanyResponseDto> companies))
            {
                companies = await _statisticsService.GetTopNCompaniesByEmployeeCountAndDateAsync(n);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(45))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                    .SetPriority(CacheItemPriority.Normal);

                _cache.Set($"get-top-n-companies-by-employee-count-{n}", companies, cacheEntryOptions);
            }

            return Ok(companies);
        }

        [HttpGet("group-companies-by-country-and-industry")]
        public async Task<ActionResult<CompanyResponseDto>> GroupCompaniesByCountryAndIndustryAsync(
            [FromQuery] string country = null, [FromQuery] string industry = null)
        {
            if (!_cache.TryGetValue($"group-companies-by-country-and-industry-{country}-{industry}", 
                out List<CompanyResponseDto> companies))
            {
                companies = await _statisticsService.GroupCompaniesByCountryAndIndustryAsync(country, industry);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(45))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                    .SetPriority(CacheItemPriority.Normal);

                _cache.Set($"group-companies-by-country-and-industry-{country}-{industry}", companies, cacheEntryOptions);
            }

            return Ok(companies);
        }
    }
}
