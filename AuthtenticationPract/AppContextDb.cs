using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // ✅ Required for IdentityDbContext
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AuthtenticationPract
{
    public class AppContextDb : IdentityDbContext<IdentityUser>
    {
        public AppContextDb(DbContextOptions<AppContextDb> options) : base(options)
        {
        }
    }
}
