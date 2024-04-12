using Diplom.DTO.AuthDtos;
using Diplom.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Diplom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (loginDto == null)
            {
                return BadRequest();
            }
            var user = await _authService.Login(loginDto);
            if (user == null)
            {
                return Unauthorized("Username or password is incorrect");
            }
            return Ok(user);

        }

    }
}
