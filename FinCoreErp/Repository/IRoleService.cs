using FinCoreErp.DTO.Authentication;

namespace FinCoreErp.Repository
{
    public interface IRoleService
    {
        Task<List<RoleDto>> GetAllRole();

        Task<RoleDto> GetRoleById(int id);

        Task CreateRole(RoleDto dto, int userId);

        Task UpdateRole(RoleDto dto, int userId);

        Task DeleteRole(int id, int userId);
    }
}
