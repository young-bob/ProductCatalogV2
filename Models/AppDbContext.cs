using Microsoft.EntityFrameworkCore;

namespace ProductCatalogV2.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Data/mydb.db;");

            //base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
