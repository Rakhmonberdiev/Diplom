using Diplom.Data;
using Diplom.DTO.AuthDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Diplom.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenService _tokenService;
       
        public AuthService(ITokenService tokenService, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            _tokenService = tokenService;
            _userManager = userManager;
            _roleManager = roleManager;

        }
        public async Task<UserDto> Login(LoginDto loginRequest)
        {
            // Поиск пользователя по имени пользователя (UserName)
            var user = await _userManager.Users.SingleOrDefaultAsync(x=>x.UserName == loginRequest.UserName);


            // Проверка пароля пользователя
            bool checkPassword = await _userManager.CheckPasswordAsync(user, loginRequest.Password);

            // Если пользователь не найден или пароль не совпадает, возвращается null
            if (user == null||checkPassword==false)
            {
                return null;
            }

            // Создание токена для пользователя
            var token = await _tokenService.CreateToken(user);

            // Создание объекта UserDto для возвращения
            var userToReturn = new UserDto
            {
                UserName = loginRequest.UserName,
                Token = token,
            };

            return userToReturn;
        }

        public async Task<UserDto> Register(RegisterDto registerDto)
        {
            // Создание нового объекта AppUser для регистрации
            AppUser user = new()
            {
                UserName = registerDto.UserName,
                NormalizedUserName = registerDto.UserName.ToUpper(),
                PhoneNumber = registerDto.PhoneNumber,
                City = registerDto.City,
            };


            // Создание пользователя с помощью UserManager
            var userCreate = await _userManager.CreateAsync(user, registerDto.Password);
            
            var roleInRoleManager = await _roleManager.FindByNameAsync("User");
            // Добавление роли пользователю
            await _userManager.AddToRoleAsync(user, roleInRoleManager.Name);
            // Создание токена для пользователя
            var token = await _tokenService.CreateToken(user);
            if (userCreate.Succeeded)
            {

                // Получение созданного пользователя для возврата
                var userToReturn = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == user.UserName);

                // Создание объекта UserDto для возвращения
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
