using Microsoft.EntityFrameworkCore;
using EntityFrameWorkPract.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace EntityFrameWorkPract
 
{
    public class AppContextDb : IdentityDbContext<IdentityUser>
    {
    public DbSet<Student>Students { get; set; }
    public AppContextDb(DbContextOptions<AppContextDb> options) : base(options) { }
    }
}
