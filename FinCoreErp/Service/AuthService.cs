using FinCoreErp.Data;
using FinCoreErp.DTO.Authentication;
using FinCoreErp.Models;
using FinCoreErp.Repository;
using Microsoft.EntityFrameworkCore;

namespace FinCoreErp.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext db;

        public AuthService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<User> Login(LoginUserDto dto)
        {
            var user = await db.Users.Include(x=>x.Role)
                .FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (user == null)
            {
                return null;
            }

            if (user.IsActive != 1)
            {
                return null;
            }

            bool passwordMatch = BCrypt.Net.BCrypt.Verify(
                dto.Password,
                user.PasswordHash
            );

            if (!passwordMatch)
            {
                return null;
            }

            return user;
        }

        public async Task Register(RegisterUserDto dto)
        {
            // Check if email already exists
            var existingUser = await db.Users
                .FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (existingUser != null)
            {
                throw new Exception("Email already exists");
            }

            // Find Employee role
            var employeeRole = await db.Roles
                .FirstOrDefaultAsync(x => x.RoleName == "Employee");

            if (employeeRole == null)
            {
                throw new Exception("Employee role not found");
            }

            // Create user
            User user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Phone = dto.Phone,
                RoleId = employeeRole.RoleId,
                IsActive = 1,
                CreatedAt = DateTime.Now
            };

            db.Users.Add(user);

            await db.SaveChangesAsync();
        }
    }
}