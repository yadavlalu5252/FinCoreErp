using FinCoreErp.DTO.Authentication;

namespace FinCoreErp.Repository
{
    public interface IAuthService
    {
        Task<bool> Register(RegisterUserDto dto);
    }
}
