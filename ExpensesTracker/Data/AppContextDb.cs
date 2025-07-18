using ExpensesTracker.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExpensesTracker.Data
{
    public class AppContextDb:IdentityDbContext<IdentityUser>
        //will add custom in future if required
    {
        public AppContextDb(DbContextOptions<AppContextDb> options) : base(options) { }
        public DbSet<ExpensesModel> Expenses { get; set; }
    }
}
