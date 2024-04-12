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

        public async Task<UserDto> Register(RegisterDto registerDto)
        {
            AppUser user = new()
            {
                UserName = registerDto.UserName,
                NormalizedUserName = registerDto.UserName.ToUpper(),
                PhoneNumber = registerDto.PhoneNumber,
                City = registerDto.City,
            };

            var userCreate = await _userManager.CreateAsync(user, registerDto.Password);
            var token = await _tokenService.CreateToken(user);
            if (userCreate.Succeeded)
            {
                var userToReturn = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == user.UserName);
                
                UserDto userDto = new()
                {
                    UserName = userToReturn.UserName,
                    Token = token
                };

                return userDto;
            }
            else
            {
                return null;
            }
        }
    }
}
