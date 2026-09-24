using Microsoft.EntityFrameworkCore;

namespace FinCoreErp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options): base(options)
        {
            
        }



    }
}
