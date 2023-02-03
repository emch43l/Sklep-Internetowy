using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sklep_Internetowy.Models;
using System.IO;

namespace Sklep_Internetowy.Contexts
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<ProductDetail> ProductsDetails { get; set; }

        public DbSet<ProductRating> ProductsRatings { get; set; }

        public DbSet<Producer> Producers { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<Cart> Carts { get; set; }


        //public string DbPath { get; set; }

        //public readonly string _DbName = "app_data.db";

        public DataContext(DbContextOptions<DataContext> options) : base(options)
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
            modelBuilder.Entity<ProductCategory>().HasIndex(c => c.Name).IsUnique();

            IdentityRole adminRole = new IdentityRole()
            {
                Name = "admin",
                NormalizedName = "ADMIN"
            };
            IdentityRole userRole = new IdentityRole()
            {
                Name = "user",
                NormalizedName = "USER"
            };

            Cart userCart = new Cart
            {
                Id = 1
            };

            Cart adminCart = new Cart
            {
                Id = 2
            };

            AppUser user = new()
            {
                FirstName = "Janusz",
                LastName = "Kowalski",
                UserName = "Kowalski",
                Email = "Kowalski@wp.pl",
                NormalizedUserName = "KOWALSKI@WP.PL",
                NormalizedEmail = "KOWALSKI@WP.PL",
                CartId = 1
            };

            AppUser admin = new()
            {
                FirstName = "Michał",
                LastName = "Mierzwa",
                UserName = "Admin",
                Email = "Admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                CartId = 2
            };

            PasswordHasher<AppUser> hasher = new PasswordHasher<AppUser>();

            admin.PasswordHash = hasher.HashPassword(admin, "admin");
            user.PasswordHash = hasher.HashPassword(user, "qaz");

            modelBuilder.Entity<Cart>().HasData(userCart, adminCart);
            modelBuilder.Entity<AppUser>().HasData(admin, user);
            modelBuilder.Entity<IdentityRole>().HasData(userRole, adminRole);

            modelBuilder.Entity<CartItem>().HasKey(ci => new { ci.ProductId, ci.CartId });


            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = user.Id,
                    RoleId = userRole.Id
                },
                new IdentityUserRole<string>
                {
                    UserId = admin.Id,
                    RoleId = adminRole.Id
                }
            );

            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory
                {
                    Id = 1,
                    Name = "Elektronika"

                },
                new ProductCategory
                {
                    Id = 2,
                    Name = "Artykuły spożywcze"
                },
                new ProductCategory
                {
                    Id = 3,
                    Name = "Budowlane"
                }
            );
            modelBuilder.Entity<Producer>().HasData(
                new Producer
                {
                    Id = 1,
                    Name = "Lays",
                    Description = "Lay's is a brand of potato chips, as well as the name of the company that founded the chip brand in the United States. The brand has also sometimes been referred to as Frito-Lay because both Lay's and Fritos are brands sold by the Frito-Lay company, which has been a wholly owned subsidiary of PepsiCo (Pepsi) since 1965. "
                },
                new Producer
                {
                    Id = 2,
                    Name = "Samsung",
                    Description = "The Samsung Group (or simply Samsung, stylized as SΛMSUNG) (Korean: 삼성 [samsʌŋ]) is a South Korean multinational manufacturing conglomerate headquartered in Samsung Town, Seoul, South Korea. It comprises numerous affiliated businesses, most of them united under the Samsung brand, and is the largest South Korean chaebol (business conglomerate). As of 2020, Samsung has the eighth highest global brand value."
                },
                new Producer
                {
                    Id = 3,
                    Name = "Default",
                    Description = "Dummy producer"
                }
            );



            modelBuilder.Entity<AppUser>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UsersRoles");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RolesClaims");
        }
    }
}
