using FinCoreErp.DTO.Authentication;
using FinCoreErp.Models;

namespace FinCoreErp.Repository
{
    public interface IAuthService
    {
        Task Register(RegisterUserDto dto);
        Task<User> Login(LoginUserDto dto);
    }
}
