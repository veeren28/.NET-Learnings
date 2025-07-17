using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JWT_Practice
{
    public class AppContextDb :IdentityDbContext
    {
        public AppContextDb(DbContextOptions<AppContextDb> options) : base(options)
        {

        }
        public DbSet<ApplicationUser> userData { get; set; }   
    }
}
