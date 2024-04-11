using Diplom.Data;

namespace Diplom.Services
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}
