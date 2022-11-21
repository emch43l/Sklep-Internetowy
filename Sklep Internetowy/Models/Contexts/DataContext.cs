using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Sklep_Internetowy.Models.Contexts
{
    public class DataContext: IdentityDbContext<IdentityUser>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }

        //public string DbPath { get; set; }

        //public readonly string _DbName = "app_data.db";

        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
            //this.DbPath = Path.Join(
            //    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            //    _DbName
            //);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    => optionsBuilder.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    Name = "Pwio",
                    Price = 2m,
                    Creation_Date = DateTime.Now,
                    Description = "Romper 7,9%",
                },
                new Product()
                {
                    Name = "Wódka",
                    Price = 29,
                    Creation_Date = DateTime.Now,
                    Description = "Eskimo 0,5 38%",
                },
                new Product()
                {
                    Name = "Czipsy",
                    Price = 5,
                    Creation_Date = DateTime.Now,
                    Description = "Lays zielona cebulka",
                },
                new Product()
                {
                    Name = "Bateria",
                    Price = 25,
                    Creation_Date = DateTime.Now,
                    Description = "Samsung 18650 3,7V MAX 20A, 3100 mAh",
                }
            );

            modelBuilder.Entity<IdentityUser>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UsersRoles");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RolesClaims");
        }
    }
}
