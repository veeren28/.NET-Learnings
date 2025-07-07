using Microsoft.EntityFrameworkCore;
using EntityFrameWorkPract.Models;

namespace EntityFrameWorkPract
 
{
    public class AppContextDb : DbContext
    {
    public DbSet<Student>Students { get; set; }
    public AppContextDb(DbContextOptions<AppContextDb> options) : base(options) { }
    }
}
