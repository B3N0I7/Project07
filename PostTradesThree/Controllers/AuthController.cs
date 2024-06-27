using Microsoft.AspNetCore.Mvc;
using PostTradesThree.Dtos;
using PostTradesThree.Services;

namespace PostTradesThree.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("SeedRoles")]
        public async Task<IActionResult> SeedRoles()
        {
            var seedRoles = await _authService.SeedRolesAsync();

            return Ok(seedRoles);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto registerDto)
        {
            var registerResult = await _authService.RegisterAsync(registerDto);

            if (registerResult.IsSucceed)
            {
                return Ok(registerResult);
            }

            return BadRequest(registerResult);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto loginDto)
        {
            var loginResult = await _authService.LoginAsync(loginDto);

            if (loginResult.IsSucceed)
            {
                return Ok(loginResult);
            }

            return Unauthorized(loginResult);
        }

        [HttpPost]
        [Route("MakeAdmin")]
        public async Task<IActionResult> MakeAdminAsync([FromBody] UpdateRoleDto updateRoleDto)
        {
            var makeAdminResult = await _authService.MakeAdminAsync(updateRoleDto);

            if (makeAdminResult.IsSucceed)
            {
                return Ok(makeAdminResult);
            }

            return BadRequest(makeAdminResult);
        }

        [HttpPost]
        [Route("MakeOwner")]
        public async Task<IActionResult> MakeOwnerAsync([FromBody] UpdateRoleDto updateRoleDto)
        {
            var makeOwnerResult = await _authService.MakeOwnerAsync(updateRoleDto);

            if (makeOwnerResult.IsSucceed)
            {
                return Ok(makeOwnerResult);
            }

            return BadRequest(makeOwnerResult);
        }

        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            await _authService.LogoutAsync();

            return Ok("Logout successful");
        }
    }
}
