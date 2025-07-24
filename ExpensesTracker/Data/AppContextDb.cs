using ExpensesTracker.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExpensesTracker.Data
{
    public class AppContextDb:IdentityDbContext<UserApplication>
        
    {
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //base.OnModelCreating(modelBuilder);

        //modelBuilder.Entity<ExpensesModel>()
        //    .HasOne(e => e.User)
        //    .WithMany()
        //    .HasForeignKey(e => e.UserId)
        //    .IsRequired();
        //}

        public AppContextDb(DbContextOptions<AppContextDb> options) : base(options) { }
        public DbSet<ExpensesModel> Expenses { get; set; }
    }
}
