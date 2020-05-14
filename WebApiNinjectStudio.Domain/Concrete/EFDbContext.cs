using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WebApiNinjectStudio.Domain.Entities;

namespace WebApiNinjectStudio.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public EFDbContext(DbContextOptions<EFDbContext> options)
            : base(options)
        { }
        public DbSet <Product> Products { get; set; }
        public DbSet <ProductImage> ProductImages { get; set; }
        public DbSet <ProductTag> ProductTags { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PassWord> PassWords { get; set; }
        public DbSet<ApiUrl> ApiUrls { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermissionApiUrl> RolePermissionApiUrls { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //one to one ProductImage -> Product
            //modelBuilder.Entity<ProductImage>()
            //    .HasOne(pi => pi.Product)
            //    .WithOne(p => p.ProductImage)
            //    .HasForeignKey<ProductImage>(pi => pi.ProductImageId);



            //one to one User -> PassWord
            modelBuilder.Entity<User>()
                .HasOne(pw => pw.PassWord)
                .WithOne(u => u.User)
                .HasForeignKey<User>(u => u.PassWordID);

            //one to many Role -> User
            modelBuilder.Entity<Role>()
                .HasMany(u => u.Users)
                .WithOne(r => r.Role)
                .HasForeignKey(u => u.RoleID);

            //many to many Role <-- RolePermissionApiUrl --> ApiUrl
            //composite key
            modelBuilder.Entity<RolePermissionApiUrl>()
                .HasKey(ra => new { ra.RoleID, ra.ApiUrlID });
            //one to many RolePermissionApiUrl -> Role
            modelBuilder.Entity<RolePermissionApiUrl>()
                .HasOne(rpa => rpa.Role)
                .WithMany(r => r.RolePermissionApiUrls)
                .HasForeignKey(rpa => rpa.RoleID);
            //one to many RolePermissionApiUrl -> ApiUrl
            modelBuilder.Entity<RolePermissionApiUrl>()
                .HasOne(rpa => rpa.ApiUrl)
                .WithMany(r => r.RolePermissionApiUrls)
                .HasForeignKey(rpa => rpa.ApiUrlID);


            //one to one Product -> ProductImage
            modelBuilder.Entity<Product>()
                .HasOne(pi=>pi.ProductImage)
                .WithOne(p => p.Product)
                .HasForeignKey<ProductImage>(pi => pi.ProductID);

            //one to many Product -> ProductTag
            modelBuilder.Entity<Product>()
                .HasMany(p => p.ProductTag)
                .WithOne(pt => pt.Product)
                .HasForeignKey(pt => pt.ProductID);

            //many to many Product <-- ProductCategory --> Category
            //composite key
            modelBuilder.Entity<ProductCategory>()
                .HasKey(pc => new { pc.ProductID, pc.CategoryId });
            //one to many ProductCategory -> Product
            modelBuilder.Entity<ProductCategory>()
                .HasOne(p => p.Product)
                .WithMany(pc => pc.ProductCategories)
                .HasForeignKey(pc => pc.ProductID);
            //one to may ProductCategory -> Category
            modelBuilder.Entity<ProductCategory>()
                .HasOne(p => p.Category)
                .WithMany(pc => pc.ProductCategories)
                .HasForeignKey(pc => pc.CategoryId);


            //Initial Data to DB
            //ApiUrl
            modelBuilder.Entity<ApiUrl>().HasData(
                new ApiUrl { ApiUrlID = 1, ApiUrlString = "/api/User", ApiRequestMethod = "Get" },
                new ApiUrl { ApiUrlID = 2, ApiUrlString = "/api/User/GetUserID", ApiRequestMethod = "GetUserID" },
                new ApiUrl { ApiUrlID = 3, ApiUrlString = "/api/product", ApiRequestMethod = "Get" },
                new ApiUrl { ApiUrlID = 4, ApiUrlString = "/api/product", ApiRequestMethod = "Post" }
            );

            //Role
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleID = 1, Name = "Administrator"},
                new Role { RoleID = 2, Name = "Guest" }
            );

            //RolePermissionApiUrl
            modelBuilder.Entity<RolePermissionApiUrl>().HasData(
                new RolePermissionApiUrl { RoleID = 1, ApiUrlID = 1 },
                new RolePermissionApiUrl { RoleID = 1, ApiUrlID = 2 },
                new RolePermissionApiUrl { RoleID = 1, ApiUrlID = 3 },
                new RolePermissionApiUrl { RoleID = 1, ApiUrlID = 4 },
                new RolePermissionApiUrl { RoleID = 2, ApiUrlID = 3 },
                new RolePermissionApiUrl { RoleID = 2, ApiUrlID = 4 }
            );

            //Password
            modelBuilder.Entity<PassWord>().HasData(
                new PassWord { PassWordID = 1, Password = "HelloWorld", Created = DateTime.Now},
                new PassWord { PassWordID = 2, Password = "Abc123", Created = DateTime.Now }
            );

            //User
            modelBuilder.Entity<User>().HasData(
                new User { UserID = 1, Email = "one@gmail.com", FirstName = "Kim", LastName = "Nielsen", PassWordID = 1, RoleID = 1 },
                new User { UserID = 2, Email = "two@gmail.com", FirstName = "Martin", LastName = "Jensen", PassWordID = 2, RoleID = 2}
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Soveværelse"},
                new Category { CategoryId = 2, CategoryName = "Badeværelse" },
                new Category { CategoryId = 3, CategoryName = "Kontor" },
                new Category { CategoryId = 4, CategoryName = "Stue" },
                new Category { CategoryId = 5, CategoryName = "Spisestue" },
                new Category { CategoryId = 6, CategoryName = "Opbevaring" },
                new Category { CategoryId = 7, CategoryName = "Have" },
                new Category { CategoryId = 8, CategoryName = "Indretning" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductID = 1,
                    Name = "Kontorstol REGSTRUP sort/grå",
                    Description = "Bremsehjul, Højdejusterbar, Nylonbase, Trinløs vippefunktion",
                    Price = 300
                },
                new Product
                {
                    ProductID = 2,
                    Name = "Barstol KLARUP sort/krom",
                    Description = "Højdejusterbar",
                    Price = 250
                }
            );

            modelBuilder.Entity<ProductImage>().HasData(
                new ProductImage { ProductImageId = 1, ProductID = 1, Url = "jysk.dk/kontor/kontorstole/basic/kontorstol-regstrup-sort-graa" },
                new ProductImage { ProductImageId = 2, ProductID = 2, Url = "jysk.dk/spisestue/barborde-stole/barstol-klarup-sort-krom-0" }
            );

            modelBuilder.Entity<ProductTag>().HasData(
                new ProductTag { ProductTagID = 1, ProductID = 1, Name = "kontorstol" },
                new ProductTag { ProductTagID = 2, ProductID = 1, Name = "sort" },
                new ProductTag { ProductTagID = 3, ProductID = 1, Name = "skum" },
                new ProductTag { ProductTagID = 4, ProductID = 1, Name = "metal" },
                new ProductTag { ProductTagID = 5, ProductID = 1, Name = "krydsfiner" },
                new ProductTag { ProductTagID = 6, ProductID = 2, Name = "kunstlæder" },
                new ProductTag { ProductTagID = 7, ProductID = 2, Name = "skum" },
                new ProductTag { ProductTagID = 8, ProductID = 2, Name = "metal" },
                new ProductTag { ProductTagID = 9, ProductID = 2, Name = "polypropylen" }
            );

            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory { CategoryId = 3 , ProductID = 1 },
                new ProductCategory { CategoryId = 5 , ProductID = 1 },
                new ProductCategory { CategoryId = 5 , ProductID = 2 }
            );

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server = 10.0.6.13; Database=test; User ID = sa; Password=Passw0rd");
        //}

    }
}
