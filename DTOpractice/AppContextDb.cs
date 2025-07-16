using DTOpractice.Models;
using Microsoft.EntityFrameworkCore;

namespace DTOpractice
{
    public class AppContextDb : DbContext
    {
        public  AppContextDb(DbContextOptions<AppContextDb> options) :base(options){}
        public DbSet<UserModel> Users { get; set; }
    }
}
