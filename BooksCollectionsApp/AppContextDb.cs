using Microsoft.EntityFrameworkCore;

namespace BooksCollectionsApp
{
    public class AppContextDb : DbContext
    {
        public AppContextDb(DbContextOptions<AppContextDb> options) : base(options)
        {
        }

        public DbSet<BookModel> Books { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }  // 🔄 Better plural name

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Category
            modelBuilder.Entity<CategoryModel>().HasData(
                new CategoryModel { Id = 1, Name = "Casual" }
            );

            // Seed Books with valid CategoryId
            modelBuilder.Entity<BookModel>().HasData(
                new BookModel { Id = 1, Author = "Mark Manson",Title="Subtle art Of not giving a Fuck", CategoryId = 1 },
                new BookModel { Id = 2, Author = "Atomic",Title="Atomic Habits", CategoryId = 1 }
            );
        }
    }
}
