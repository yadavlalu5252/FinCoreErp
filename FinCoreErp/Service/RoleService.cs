using FinCoreErp.Data;
using FinCoreErp.DTO.Authentication;
using FinCoreErp.Models;
using FinCoreErp.Repository;
using Microsoft.EntityFrameworkCore;

namespace FinCoreErp.Service
{
    public class RoleService : IRoleService
    {
        private readonly AppDbContext db;

        public RoleService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<List<RoleDto>> GetAllRole()
        {
            var data = await db.Roles
                .Select(x => new RoleDto
                {
                    RoleId = x.RoleId,
                    RoleName = x.RoleName,
                    Description = x.Description,
                    IsActive = x.IsActive ?? 0
                }).ToListAsync();

            return data;
        }

        public async Task<RoleDto> GetRoleById(int id)
        {
            var role = await db.Roles.FirstOrDefaultAsync(x => x.RoleId == id);

            if (role == null)
            {
                return null;
            }

            return new RoleDto
            {
                RoleId = role.RoleId,
                RoleName = role.RoleName,
                Description = role.Description,
                IsActive = role.IsActive ?? 0
            };
        }

        public async Task CreateRole(RoleDto dto, int userId)
        {
            var existingRole = await db.Roles.FirstOrDefaultAsync(x =>
                    x.RoleName == dto.RoleName);

            if (existingRole != null)
            {
                throw new Exception("Role already exists.");
            }

            var role = new Role
            {
                RoleName = dto.RoleName,
                Description = dto.Description,
                IsActive = dto.IsActive,

                CreatedAt = DateTime.Now,
                CreatedBy = userId
            };

            db.Roles.Add(role);

            await db.SaveChangesAsync();
        }

        public async Task UpdateRole(RoleDto dto, int userId)
        {
            var role = await db.Roles.FirstOrDefaultAsync(x => x.RoleId == dto.RoleId);

            if (role == null)
            {
                throw new Exception("Role not found.");
            }

            role.RoleName = dto.RoleName;
            role.Description = dto.Description;
            role.IsActive = dto.IsActive;

            role.ModifiedAt = DateTime.Now;
            role.ModifiedBy = userId;

            await db.SaveChangesAsync();
        }

        public async Task DeleteRole(int id, int userId)
        {
            var role = await db.Roles.FirstOrDefaultAsync(x => x.RoleId == id);

            if (role == null)
            {
                throw new Exception("Role not found.");
            }

            role.IsActive = 0;
            role.ModifiedAt = DateTime.Now;
            role.ModifiedBy = userId;
            await db.SaveChangesAsync();
        }
    }
}
