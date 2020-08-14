using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiNinjectStudio.Domain.Entities;

namespace WebApiNinjectStudio.Domain.Concrete
{
    public class EFDbContext : DbContext
    {        

        public EFDbContext(DbContextOptions<EFDbContext> options)
            : base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ApiUrl> ApiUrls { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermissionApiUrl> RolePermissionApiUrls { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region Database relationships
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
                .HasOne(pi => pi.ProductImage)
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

            #endregion

            #region  Soft Delete using Query Filters
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                //Add the IsDeleted property
                entityType.AddProperty("IsDeleted", typeof(bool));

                //Create the query filter
                var parameter = Expression.Parameter(entityType.ClrType);

                //EF.Property<bool>(post, "IsDeleted")
                var propertyMethodInfo = typeof(EF).GetMethod("Property").MakeGenericMethod(typeof(bool));
                var isDeletedProperty = Expression.Call(propertyMethodInfo, parameter, Expression.Constant("IsDeleted"));

                //EF.Property<bool>(post, "IsDeleted") == false
                var compareExpression = Expression.MakeBinary(ExpressionType.Equal, isDeletedProperty, Expression.Constant(false));

                //post => EF.Property<bool>(post, "IsDeleted") == false
                var lambda = Expression.Lambda(compareExpression, parameter);

                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }
            #endregion

            #region Initial Data 

            //ApiUrl
            modelBuilder.Entity<ApiUrl>().HasData(
                new { ApiUrlID = 1, ApiUrlString = "/api/User", ApiRequestMethod = "Get", IsDeleted = false},
                new { ApiUrlID = 2, ApiUrlString = "/api/User/GetUserID", ApiRequestMethod = "Get", IsDeleted = false},
                new { ApiUrlID = 3, ApiUrlString = "/api/product", ApiRequestMethod = "Get", IsDeleted = false },
                new { ApiUrlID = 4, ApiUrlString = "/api/product", ApiRequestMethod = "Post", IsDeleted = false},
                //Categories
                new { ApiUrlID = 5, ApiRequestMethod = "Post", ApiUrlString = @"/api/v1/categories", ApiUrlRegex = @"^\/api\/v1\/categories$", Description = "Create category", IsDeleted = false },
                new { ApiUrlID = 6, ApiRequestMethod = "Get", ApiUrlString = @"/api/v1/categories", ApiUrlRegex = @"^\/api\/v1\/categories$", Description = "Get category list", IsDeleted = false },
                new { ApiUrlID = 7, ApiRequestMethod = "Get", ApiUrlString = @"/api/v1/categories/{categoryId}", ApiUrlRegex = @"^\/api\/v1\/categories\/\d+$", Description = "Get info about category by category id", IsDeleted = false },
                new { ApiUrlID = 8, ApiRequestMethod = "Delete", ApiUrlString = @"/api/v1/categories/{categoryId}", ApiUrlRegex = @"^\/api\/v1\/categories\/\d+$", Description = "Delete category by identifier", IsDeleted = false },
                new { ApiUrlID = 9, ApiRequestMethod = "Put", ApiUrlString = @"/api/v1/categories/{categoryId}", ApiUrlRegex = @"^\/api\/v1\/categories\/\d+$", Description = "Update category by identifier", IsDeleted = false },
                new { ApiUrlID = 10, ApiRequestMethod = "Post", ApiUrlString = @"/api/v1/categories/{categoryId}/products", ApiUrlRegex = @"^\/api\/v1\/categories\/\d+\/products$", Description = "Assign a product to the required category", IsDeleted = false },
                new { ApiUrlID = 11, ApiRequestMethod = "Get", ApiUrlString = @"/api/v1/categories/{categoryId}/products", ApiUrlRegex = @"^\/api\/v1\/categories\/\d+\/products(\?.*)*$", Description = "Get products assigned to category", IsDeleted = false },
                new { ApiUrlID = 12, ApiRequestMethod = "Put", ApiUrlString = @"/api/v1/categories/{categoryId}/products", ApiUrlRegex = @"^\/api\/v1\/categories\/\d+\/products$", Description = "Assign a product to the required category", IsDeleted = false },
                new { ApiUrlID = 13, ApiRequestMethod = "Delete", ApiUrlString = @"/api/v1/categories/{categoryId}/products/{productId}", ApiUrlRegex = @"^\/api\/v1\/categories\/\d+\/products\/\d+$", Description = "Remove the product assignment from the category by category id and product id", IsDeleted = false },
                //Products
                new { ApiUrlID = 14, ApiRequestMethod = "Get", ApiUrlString = @"/api/v1/products", ApiUrlRegex = @"^\/api\/v1\/products(\?.*)*$", Description = "Get product list", IsDeleted = false },
                new { ApiUrlID = 15, ApiRequestMethod = "Post", ApiUrlString = @"/api/v1/products", ApiUrlRegex = @"^\/api\/v1\/products$", Description = "Create product", IsDeleted = false },
                new { ApiUrlID = 16, ApiRequestMethod = "Get", ApiUrlString = @"/api/v1/products/{productId}", ApiUrlRegex = @"^\/api\/v1\/products\/\d+$", Description = "Get info about product by product id", IsDeleted = false },
                new { ApiUrlID = 17, ApiRequestMethod = "Put", ApiUrlString = @"/api/v1/products/{productId}", ApiUrlRegex = @"^\/api\/v1\/products\/\d+$", Description = "Update the product by product id", IsDeleted = false },
                new { ApiUrlID = 18, ApiRequestMethod = "Delete", ApiUrlString = "/api/v1/products/{productId}", ApiUrlRegex = @"^\/api\/v1\/products\/\d+$", Description = "Remove the product by product id", IsDeleted = false },
                // integrations
                new { ApiUrlID = 19, ApiRequestMethod = "Get", ApiUrlString = @"/api/v1/integrations/customer/userid", ApiUrlRegex = @"^\/api\/v1\/integrations\/customer\/userid$", Description = "Get id of current user.", IsDeleted = false }
            );


            //Role
            modelBuilder.Entity<Role>().HasData(
                new { RoleID = 1, Name = "Administrator", IsDeleted = false },
                new { RoleID = 2, Name = "Guest", IsDeleted = false }
            );

            //RolePermissionApiUrl
            modelBuilder.Entity<RolePermissionApiUrl>().HasData(
                new { RoleID = 1, ApiUrlID = 1, IsDeleted = false },
                new { RoleID = 1, ApiUrlID = 2, IsDeleted = false },
                new { RoleID = 1, ApiUrlID = 3, IsDeleted = false },
                new { RoleID = 1, ApiUrlID = 4, IsDeleted = false },
                new { RoleID = 1, ApiUrlID = 5, IsDeleted = false },
                new { RoleID = 1, ApiUrlID = 6, IsDeleted = false },
                new { RoleID = 1, ApiUrlID = 7, IsDeleted = false },
                new { RoleID = 1, ApiUrlID = 8, IsDeleted = false },
                new { RoleID = 1, ApiUrlID = 9, IsDeleted = false },
                new { RoleID = 1, ApiUrlID = 10, IsDeleted = false },
                new { RoleID = 1, ApiUrlID = 11, IsDeleted = false },
                new { RoleID = 1, ApiUrlID = 12, IsDeleted = false },
                new { RoleID = 1, ApiUrlID = 13, IsDeleted = false },
                new { RoleID = 1, ApiUrlID = 14, IsDeleted = false },
                new { RoleID = 1, ApiUrlID = 15, IsDeleted = false },
                new { RoleID = 1, ApiUrlID = 16, IsDeleted = false },
                new { RoleID = 1, ApiUrlID = 17, IsDeleted = false },
                new { RoleID = 1, ApiUrlID = 18, IsDeleted = false },
                new { RoleID = 1, ApiUrlID = 19, IsDeleted = false },
                new { RoleID = 2, ApiUrlID = 3, IsDeleted = false },
                new { RoleID = 2, ApiUrlID = 4, IsDeleted = false }
            );

            //User
            modelBuilder.Entity<User>().HasData(
                //Password = "HelloWorld"
                new
                {
                    UserID = 1,
                    Email = "one@gmail.com",
                    FirstName = "Kim",
                    LastName = "Nielsen",
                    Password = "M4jZrsPV2wNAeOH1YooKUdALx6Ek0DJaMf8yoiYI0Mc=",
                    RoleID = 1,
                    IsDeleted = false
                },
                //Password = "Abc123"
                new
                {
                    UserID = 2,
                    Email = "two@gmail.com",
                    FirstName = "Martin",
                    LastName = "Jensen",
                    Password = "FOHqRDbYuVdIBvLS6r2YMVU4Yu7E54DJJJxrWGh5YZc=",
                    RoleID = 2,
                    IsDeleted = false
                }
            );

            modelBuilder.Entity<Category>().HasData(
                new { CategoryId = 1, CategoryName = "Soveværelse", IsDeleted = false },
                new { CategoryId = 2, CategoryName = "Badeværelse", IsDeleted = false },
                new { CategoryId = 3, CategoryName = "Kontor", IsDeleted = false },
                new { CategoryId = 4, CategoryName = "Stue", IsDeleted = false },
                new { CategoryId = 5, CategoryName = "Spisestue", IsDeleted = false },
                new { CategoryId = 6, CategoryName = "Opbevaring", IsDeleted = false },
                new { CategoryId = 7, CategoryName = "Have", IsDeleted = false },
                new { CategoryId = 8, CategoryName = "Indretning", IsDeleted = false }
            );

            modelBuilder.Entity<Product>().HasData(
                new 
                {
                    ProductID = 1,
                    Name = "Kontorstol REGSTRUP sort/grå",
                    Description = "Bremsehjul, Højdejusterbar, Nylonbase, Trinløs vippefunktion",
                    Price = 300m,
                    IsDeleted = false
                },
                new 
                {
                    ProductID = 2,
                    Name = "Barstol KLARUP sort/krom",
                    Description = "Højdejusterbar",
                    Price = 250m,
                    IsDeleted = false
                }
            );

            modelBuilder.Entity<ProductImage>().HasData(
                new 
                {
                    ProductImageId = 1,
                    ProductID = 1,
                    Url = "jysk.dk/kontor/kontorstole/basic/kontorstol-regstrup-sort-graa",
                    IsDeleted = false
                },
                new 
                {
                    ProductImageId = 2,
                    ProductID = 2,
                    Url = "jysk.dk/spisestue/barborde-stole/barstol-klarup-sort-krom-0",
                    IsDeleted = false
                }
            );

            modelBuilder.Entity<ProductTag>().HasData(
                new { ProductTagID = 1, ProductID = 1, Name = "kontorstol", IsDeleted = false },
                new { ProductTagID = 2, ProductID = 1, Name = "sort", IsDeleted = false },
                new { ProductTagID = 3, ProductID = 1, Name = "skum", IsDeleted = false },
                new { ProductTagID = 4, ProductID = 1, Name = "metal", IsDeleted = false },
                new { ProductTagID = 5, ProductID = 1, Name = "krydsfiner", IsDeleted = false },
                new { ProductTagID = 6, ProductID = 2, Name = "kunstlæder", IsDeleted = false },
                new { ProductTagID = 7, ProductID = 2, Name = "skum", IsDeleted = false },
                new { ProductTagID = 8, ProductID = 2, Name = "metal", IsDeleted = false },
                new { ProductTagID = 9, ProductID = 2, Name = "polypropylen", IsDeleted = false }
            );

            modelBuilder.Entity<ProductCategory>().HasData(
                new { CategoryId = 3, ProductID = 1, IsDeleted = false },
                new { CategoryId = 5, ProductID = 1, IsDeleted = false },
                new { CategoryId = 5, ProductID = 2, IsDeleted = false }
            );

            #endregion
            

            base.OnModelCreating(modelBuilder);
        }


        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server = 10.0.6.13; Database=test; User ID = sa; Password=Passw0rd");
        //}

    }
}
