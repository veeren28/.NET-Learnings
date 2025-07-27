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
        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            base.OnModelCreating(builder);

            builder.Entity<CategoryModel>().HasData(
                new CategoryModel { Id = 1, CategoryName = "Food" },
                new CategoryModel { Id = 2, CategoryName = "Travelling" },
                new CategoryModel { Id = 3, CategoryName = "Medical" }

                );
        }


        public AppContextDb(DbContextOptions<AppContextDb> options) : base(options) { }
        public DbSet<ExpensesModel> Expenses { get; set; }
        public DbSet<CategoryModel> Category { get; set; }
        public DbSet<IncomeModel> Income { get; set; }
    }
}
