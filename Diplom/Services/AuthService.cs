using Diplom.Data;
using Diplom.DTO.AuthDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Diplom.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
       
        public AuthService(ITokenService tokenService, UserManager<AppUser> userManager)
        {

            _tokenService = tokenService;
            _userManager = userManager;

        }
        public async Task<UserDto> Login(LoginDto loginRequest)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(x=>x.UserName == loginRequest.UserName);
            bool checkPassword = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
            if (user == null||checkPassword==false)
            {
                return null;
            }
            var token = await _tokenService.CreateToken(user);

            var userToReturn = new UserDto
            {
                UserName = loginRequest.UserName,
                Token = token,
            };

            return userToReturn;
        }

        public Task<UserDto> Register(RegisterDto registerDto)
        {
            throw new NotImplementedException();
        }
    }
}
