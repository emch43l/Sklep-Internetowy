using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Sklep_Internetowy.Models.Contexts
{
    public class AppDbContext: DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }
        public string DbPath { get; set; }
        
        public AppDbContext()
        {
            this.DbPath = Path.Join(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), 
                "app.db"
                );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
            => optionsBuilder.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    Id = 1,
                    Name = "Pwio",
                    Price = 2m,
                    Creation_Date = DateTime.Now,
                    Description = "Romper 7,9%",
                },
                new Product()
                {
                    Id = 2,
                    Name = "Wódka",
                    Price = 29,
                    Creation_Date = DateTime.Now,
                    Description = "Eskimo 0,5 38%",
                },
                new Product()
                {
                    Id = 3,
                    Name = "Czipsy",
                    Price = 5,
                    Creation_Date = DateTime.Now,
                    Description = "Lays zielona cebulka",
                },
                new Product()
                {
                    Id = 4,
                    Name = "Bateria",
                    Price = 25,
                    Creation_Date = DateTime.Now,
                    Description = "Samsung 18650 3,7V MAX 20A, 3100 mAh",
                }
            ) ;
        }
    }
}
