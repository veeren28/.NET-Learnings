using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RoleBasedAuthentication
{
    public class AppContextDb :IdentityDbContext<IdentityUser,IdentityRole,string>
    { 
        public AppContextDb(DbContextOptions<AppContextDb> options) : base(options) { }
     
    }
}
