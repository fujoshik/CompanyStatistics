using CompanyStatistics.Domain.Abstraction.Services;
using CompanyStatistics.Domain.DTOs.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompanyStatistics.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _service;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _service = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginDto account)
        {
            var token = await _service.LoginAsync(account);

            if (token is null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync([FromBody] RegisterDto registerDto)
        {
            await _service.RegisterAccountAsync(registerDto);

            return NoContent();
        }
    }
}
