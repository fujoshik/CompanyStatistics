using CompanyStatistics.API.Configuration;
using CompanyStatistics.Domain.Abstraction.Services;
using CompanyStatistics.Domain.DTOs.User;
using CompanyStatistics.Domain.Enums;
using CompanyStatistics.Domain.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace CompanyStatistics.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    [AuthorizeRoles(Role.Admin)]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        [ActionName(nameof(GetByIdAsync))]
        public async Task<ActionResult<UserResponseDto>> GetByIdAsync([FromRoute] string id)
        {
            var user = await _userService.GetByIdAsync(id);

            return Ok(user);
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResult<UserResponseDto>>> GetPageAsync(
            [FromQuery] PagingInfo pagingInfo)
        {
            var users = await _userService.GetPageAsync(pagingInfo);

            return Ok(users);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] string id, [FromBody] UserWithoutIdDto user)
        {
            await _userService.UpdateAsync(id, user);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [AuthorizeRoles(Role.Admin)]
        public async Task<ActionResult> DeleteAsync([FromRoute] string id)
        {
            await _userService.DeleteAsync(id);

            return NoContent();
        }
    }
}
