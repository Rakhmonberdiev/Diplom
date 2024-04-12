using Diplom.Data;
using Diplom.DTO.AuthDtos;
using Diplom.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Diplom.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<AppUser> _userManager;
        public AuthController(IAuthService authService, UserManager<AppUser> userManager)
        {
            _authService = authService;
            _userManager = userManager;
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
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

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if(await UserExists(registerDto.UserName))
            {
                return BadRequest("Username is taken");
            }
            var rs = await _authService.Register(registerDto);
            if (rs == null)
            {
                return BadRequest("Failed to create user.");
            }
            return Ok(rs);
        }
        private async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(x=>x.UserName == username.ToLower());
        }
    }
}
