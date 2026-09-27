using System.Threading.Tasks;
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
        public async Task<bool> Register(RegisterUserDto dto)
        {
            var existingUser = await db.Users.FirstOrDefaultAsync(x => x.Email == dto.Email);
<<<<<<< Updated upstream
            if(existingUser != null)
            {
                return false;
            }
=======
            if (existingUser != null)
            {
                return false;
            }

>>>>>>> Stashed changes
            User user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Phone = dto.Phone,
                IsActive = 1,
                CreatedAt = DateTime.Now
            };

            db.Users.Add(user);
            await db.SaveChangesAsync();
            return true;
        }
    }
}
