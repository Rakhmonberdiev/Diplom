using Diplom.DTO.AuthDtos;

namespace Diplom.Services
{
    public interface IAuthService
    {
        Task<UserDto> Register(RegisterDto registerDto);
        Task<UserDto> Login(LoginDto loginRequest);
    }
}
