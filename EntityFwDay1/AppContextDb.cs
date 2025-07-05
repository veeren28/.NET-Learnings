using Microsoft.EntityFrameworkCore;
using EntityFwDay1.Models;
namespace EntityFwDay1
{
    public class AppContextDb :DbContext
    {
        public DbSet<Student> students {  get; set; }
        public AppContextDb(DbContextOptions<AppContextDb> options) : base(options) { }
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Serve=\SQLEXPRESS;Database=EnitityDb;Trusted_Connection=True");
        //}

        
    }
}
