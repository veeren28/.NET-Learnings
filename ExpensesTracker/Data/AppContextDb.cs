using ExpensesTracker.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ExpensesTracker.Data
{
    public class AppContextDb : IdentityDbContext<UserApplication>
    {
        public AppContextDb(DbContextOptions<AppContextDb> options) : base(options)
        {
        }

        // DbSets
        public DbSet<IncomeModel> Incomes { get; set; }
        public DbSet<ExpensesModel> Expenses { get; set; }
        public DbSet<TransactionModel> Transactions { get; set; }
        public DbSet<CategoryModel> Category { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<CategoryModel>().HasData(
    new CategoryModel { Id = 1, CategoryName = "Food" },
    new CategoryModel { Id = 2, CategoryName = "Transport" },
    new CategoryModel { Id = 3, CategoryName = "Utilities" },
    new CategoryModel { Id = 4, CategoryName = "Health" },
    new CategoryModel { Id = 5, CategoryName = "Entertainment" },
    new CategoryModel { Id = 6, CategoryName = "Shopping" },
    new CategoryModel { Id = 7, CategoryName = "Education" },
    new CategoryModel { Id = 8, CategoryName = "Salary" },
    new CategoryModel { Id = 9, CategoryName = "Investment" },
    new CategoryModel { Id = 10, CategoryName = "Other" }
);

            // User ↔ Income: One user can have many incomes
            builder.Entity<IncomeModel>()
                .HasOne(i => i.User)
                .WithMany()
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Prevents deleting user if they have incomes

            // User ↔ Expenses: One user can have many expenses
            builder.Entity<ExpensesModel>()
                .HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Prevents deleting user if they have expenses

            // Income ↔ Transaction: One-to-One with Cascade Delete
            builder.Entity<IncomeModel>()
                .HasOne(i => i.Transaction)
                .WithOne()
                .HasForeignKey<IncomeModel>(i => i.TransactionId)
                .OnDelete(DeleteBehavior.NoAction); // Deleting income deletes related transaction

            // Expenses ↔ Transaction: One-to-One with Cascade Delete
            builder.Entity<ExpensesModel>()
                .HasOne(e => e.Transaction)
                .WithOne()
                .HasForeignKey<ExpensesModel>(e => e.TransactionId)
                .OnDelete(DeleteBehavior.NoAction); // Deleting expense deletes related transaction


            //Expense to Category

           
        }
    }
}
