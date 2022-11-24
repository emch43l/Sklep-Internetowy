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

        public DbSet<ProductDetail> ProductsDetails { get; set; }

        public DbSet<ProductRating> ProductsRatings { get; set; }

        public DbSet<Producer> Producers { get; set; }

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
            modelBuilder.Entity<ProductDetail>().HasOne(p => p.Product).WithOne(p => p.ProductDetail);
            modelBuilder.Entity<Producer>().HasData(
                new Producer
                {
                    Id = 1,
                    Name = "Lays"
                },
                new Producer
                {
                    Id = 2,
                    Name = "Samsung"
                },
                new Producer
                {
                    Id = 3,
                    Name = "Default"
                }
            );
            //modelBuilder.Entity<Producer>().HasData(
            //    new Producer
            //    {
            //        Id = 1,
            //        Name = "Tyskie"
            //    },
            //    new Producer
            //    {
            //        Id = 2,
            //        Name = "Finlandia"
            //    },
            //    new Producer
            //    {
            //        Id = 3,
            //        Name = "Lays"
            //    },
            //    new Producer
            //    {
            //        Id = 4,
            //        Name = "Samsung"
            //    }
            //);
            //modelBuilder.Entity<Product>().HasData(
            //    new Product()
            //    {
            //        Id = 1,
            //        Name = "Pwio",
            //        Price = 2m,
            //        ProductDetail = new ProductDetail
            //        {
            //            Id = 1,
            //            Creation_Date = DateTime.Now,
            //            Description = "Romper 7,9%",
            //        }
            //    },
            //    new Product()
            //    {
            //        Id = 2,
            //        Name = "Wódka",
            //        Price = 29,
            //        ProductDetail = new ProductDetail
            //        {
            //            Id = 2,
            //            Creation_Date = DateTime.Now,
            //            Description = "Eskimo 0,5 38%",
            //        }
            //    },
            //    new Product()
            //    {
            //        Id = 3,
            //        Name = "Czipsy",
            //        Price = 5,
            //        ProductDetail = new ProductDetail
            //        {
            //            Id = 3,
            //            Creation_Date = DateTime.Now,
            //            Description = "Lays zielona cebulka",
            //        }
            //    },
            //    new Product()
            //    {
            //        Id = 4,
            //        Name = "Bateria",
            //        Price = 25,
            //        ProductDetail = new ProductDetail
            //        {
            //            Id = 4,
            //            Creation_Date = DateTime.Now,
            //            Description = "Samsung 18650 3,7V MAX 20A, 3100 mAh",

            //        }
                    
            //    }
            //);



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
