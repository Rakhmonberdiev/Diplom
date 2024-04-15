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

        // Обработчик HTTP POST запроса для входа пользователя в систему
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            // Проверка, передан ли объект loginDto
            if (loginDto == null)
            {
                // Если объект loginDto отсутствует, возвращается код 400 (BadRequest)
                return BadRequest();
            }
            // Вызов метода Login сервиса аутентификации для выполнения входа пользователя
            var user = await _authService.Login(loginDto);
            // Проверка, успешно ли выполнен вход пользователя
            if (user == null)
            {
                // Если вход не выполнен, возвращается код 401 (Unauthorized) с сообщением об ошибке
                return Unauthorized("Имя пользователя или пароль неверны");
            }
            // Возвращение кода 200 (OK) с объектом user в качестве результата
            return Ok(user);

        }


        // Обработчик HTTP POST запроса для регистрации нового пользователя
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            // Проверка, существует ли уже пользователь с указанным именем пользователя (username)
            if (await UserExists(registerDto.UserName))
            {
                // Если пользователь существует, возвращается код 400 (BadRequest) с сообщением об ошибке
                return BadRequest("Имя пользователя занято");
            }

            // Вызов метода Register сервиса аутентификации для создания нового пользователя
            var rs = await _authService.Register(registerDto);


            // Проверка, успешно ли создан пользователь
            if (rs == null)
            {
                // Если создание пользователя не удалось, возвращается код 400 (BadRequest) с сообщением об ошибке
                return BadRequest("Не удалось создать пользователя.");
            }

            // Возвращение кода 200 (OK) с объектом rs в качестве результата
            return Ok(rs);
        }

        // Метод для проверки существования пользователя с указанным именем пользователя (username)
        private async Task<bool> UserExists(string username)
        {
            // Проверка, существует ли пользователь с указанным именем пользователя
            return await _userManager.Users.AnyAsync(x=>x.UserName == username.ToLower());
        }
    }
}
