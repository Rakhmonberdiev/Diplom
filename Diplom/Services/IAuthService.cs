using Diplom.DTO.AuthDtos;

namespace Diplom.Services
{
    public interface IAuthService
    {
        // Метод для регистрации пользователя
        Task<UserDto> Register(RegisterDto registerDto);

        // Метод для аутентификации пользователя
        Task<UserDto> Login(LoginDto loginRequest);
    }
}
