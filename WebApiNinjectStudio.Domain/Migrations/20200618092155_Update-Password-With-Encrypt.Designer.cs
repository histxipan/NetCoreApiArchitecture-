﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApiNinjectStudio.Domain.Concrete;

namespace WebApiNinjectStudio.Domain.Migrations
{
    [DbContext(typeof(EFDbContext))]
    [Migration("20200618092155_Update-Password-With-Encrypt")]
    partial class UpdatePasswordWithEncrypt
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApiNinjectStudio.Domain.Entities.ApiUrl", b =>
                {
                    b.Property<int>("ApiUrlID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApiRequestMethod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ApiUrlString")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ApiUrlID");

                    b.ToTable("ApiUrls");

                    b.HasData(
                        new
                        {
                            ApiUrlID = 1,
                            ApiRequestMethod = "Get",
                            ApiUrlString = "/api/User"
                        },
                        new
                        {
                            ApiUrlID = 2,
                            ApiRequestMethod = "GetUserID",
                            ApiUrlString = "/api/User/GetUserID"
                        },
                        new
                        {
                            ApiUrlID = 3,
                            ApiRequestMethod = "Get",
                            ApiUrlString = "/api/product"
                        },
                        new
                        {
                            ApiUrlID = 4,
                            ApiRequestMethod = "Post",
                            ApiUrlString = "/api/product"
                        });
                });

            modelBuilder.Entity("WebApiNinjectStudio.Domain.Entities.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            CategoryName = "Soveværelse"
                        },
                        new
                        {
                            CategoryId = 2,
                            CategoryName = "Badeværelse"
                        },
                        new
                        {
                            CategoryId = 3,
                            CategoryName = "Kontor"
                        },
                        new
                        {
                            CategoryId = 4,
                            CategoryName = "Stue"
                        },
                        new
                        {
                            CategoryId = 5,
                            CategoryName = "Spisestue"
                        },
                        new
                        {
                            CategoryId = 6,
                            CategoryName = "Opbevaring"
                        },
                        new
                        {
                            CategoryId = 7,
                            CategoryName = "Have"
                        },
                        new
                        {
                            CategoryId = 8,
                            CategoryName = "Indretning"
                        });
                });

            modelBuilder.Entity("WebApiNinjectStudio.Domain.Entities.PassWord", b =>
                {
                    b.Property<int>("PassWordID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PassWordID");

                    b.ToTable("PassWords");

                    b.HasData(
                        new
                        {
                            PassWordID = 1,
                            Created = new DateTime(2020, 6, 18, 11, 21, 52, 70, DateTimeKind.Local).AddTicks(6149),
                            Password = "M4jZrsPV2wNAeOH1YooKUdALx6Ek0DJaMf8yoiYI0Mc="
                        },
                        new
                        {
                            PassWordID = 2,
                            Created = new DateTime(2020, 6, 18, 11, 21, 52, 87, DateTimeKind.Local).AddTicks(1268),
                            Password = "FOHqRDbYuVdIBvLS6r2YMVU4Yu7E54DJJJxrWGh5YZc="
                        });
                });

            modelBuilder.Entity("WebApiNinjectStudio.Domain.Entities.Product", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ProductID");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductID = 1,
                            Description = "Bremsehjul, Højdejusterbar, Nylonbase, Trinløs vippefunktion",
                            Name = "Kontorstol REGSTRUP sort/grå",
                            Price = 300m
                        },
                        new
                        {
                            ProductID = 2,
                            Description = "Højdejusterbar",
                            Name = "Barstol KLARUP sort/krom",
                            Price = 250m
                        });
                });

            modelBuilder.Entity("WebApiNinjectStudio.Domain.Entities.ProductCategory", b =>
                {
                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.HasKey("ProductID", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("ProductCategories");

                    b.HasData(
                        new
                        {
                            ProductID = 1,
                            CategoryId = 3
                        },
                        new
                        {
                            ProductID = 1,
                            CategoryId = 5
                        },
                        new
                        {
                            ProductID = 2,
                            CategoryId = 5
                        });
                });

            modelBuilder.Entity("WebApiNinjectStudio.Domain.Entities.ProductImage", b =>
                {
                    b.Property<int>("ProductImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductImageId");

                    b.HasIndex("ProductID")
                        .IsUnique();

                    b.ToTable("ProductImages");

                    b.HasData(
                        new
                        {
                            ProductImageId = 1,
                            ProductID = 1,
                            Url = "jysk.dk/kontor/kontorstole/basic/kontorstol-regstrup-sort-graa"
                        },
                        new
                        {
                            ProductImageId = 2,
                            ProductID = 2,
                            Url = "jysk.dk/spisestue/barborde-stole/barstol-klarup-sort-krom-0"
                        });
                });

            modelBuilder.Entity("WebApiNinjectStudio.Domain.Entities.ProductTag", b =>
                {
                    b.Property<int>("ProductTagID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.HasKey("ProductTagID");

                    b.HasIndex("ProductID");

                    b.ToTable("ProductTags");

                    b.HasData(
                        new
                        {
                            ProductTagID = 1,
                            Name = "kontorstol",
                            ProductID = 1
                        },
                        new
                        {
                            ProductTagID = 2,
                            Name = "sort",
                            ProductID = 1
                        },
                        new
                        {
                            ProductTagID = 3,
                            Name = "skum",
                            ProductID = 1
                        },
                        new
                        {
                            ProductTagID = 4,
                            Name = "metal",
                            ProductID = 1
                        },
                        new
                        {
                            ProductTagID = 5,
                            Name = "krydsfiner",
                            ProductID = 1
                        },
                        new
                        {
                            ProductTagID = 6,
                            Name = "kunstlæder",
                            ProductID = 2
                        },
                        new
                        {
                            ProductTagID = 7,
                            Name = "skum",
                            ProductID = 2
                        },
                        new
                        {
                            ProductTagID = 8,
                            Name = "metal",
                            ProductID = 2
                        },
                        new
                        {
                            ProductTagID = 9,
                            Name = "polypropylen",
                            ProductID = 2
                        });
                });

            modelBuilder.Entity("WebApiNinjectStudio.Domain.Entities.Role", b =>
                {
                    b.Property<int>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleID");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            RoleID = 1,
                            Name = "Administrator"
                        },
                        new
                        {
                            RoleID = 2,
                            Name = "Guest"
                        });
                });

            modelBuilder.Entity("WebApiNinjectStudio.Domain.Entities.RolePermissionApiUrl", b =>
                {
                    b.Property<int>("RoleID")
                        .HasColumnType("int");

                    b.Property<int>("ApiUrlID")
                        .HasColumnType("int");

                    b.HasKey("RoleID", "ApiUrlID");

                    b.HasIndex("ApiUrlID");

                    b.ToTable("RolePermissionApiUrls");

                    b.HasData(
                        new
                        {
                            RoleID = 1,
                            ApiUrlID = 1
                        },
                        new
                        {
                            RoleID = 1,
                            ApiUrlID = 2
                        },
                        new
                        {
                            RoleID = 1,
                            ApiUrlID = 3
                        },
                        new
                        {
                            RoleID = 1,
                            ApiUrlID = 4
                        },
                        new
                        {
                            RoleID = 2,
                            ApiUrlID = 3
                        },
                        new
                        {
                            RoleID = 2,
                            ApiUrlID = 4
                        });
                });

            modelBuilder.Entity("WebApiNinjectStudio.Domain.Entities.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PassWordID")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleID")
                        .HasColumnType("int");

                    b.HasKey("UserID");

                    b.HasIndex("PassWordID")
                        .IsUnique();

                    b.HasIndex("RoleID");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserID = 1,
                            Email = "one@gmail.com",
                            FirstName = "Kim",
                            LastName = "Nielsen",
                            PassWordID = 1,
                            RoleID = 1
                        },
                        new
                        {
                            UserID = 2,
                            Email = "two@gmail.com",
                            FirstName = "Martin",
                            LastName = "Jensen",
                            PassWordID = 2,
                            RoleID = 2
                        });
                });

            modelBuilder.Entity("WebApiNinjectStudio.Domain.Entities.ProductCategory", b =>
                {
                    b.HasOne("WebApiNinjectStudio.Domain.Entities.Category", "Category")
                        .WithMany("ProductCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApiNinjectStudio.Domain.Entities.Product", "Product")
                        .WithMany("ProductCategories")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApiNinjectStudio.Domain.Entities.ProductImage", b =>
                {
                    b.HasOne("WebApiNinjectStudio.Domain.Entities.Product", "Product")
                        .WithOne("ProductImage")
                        .HasForeignKey("WebApiNinjectStudio.Domain.Entities.ProductImage", "ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApiNinjectStudio.Domain.Entities.ProductTag", b =>
                {
                    b.HasOne("WebApiNinjectStudio.Domain.Entities.Product", "Product")
                        .WithMany("ProductTag")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApiNinjectStudio.Domain.Entities.RolePermissionApiUrl", b =>
                {
                    b.HasOne("WebApiNinjectStudio.Domain.Entities.ApiUrl", "ApiUrl")
                        .WithMany("RolePermissionApiUrls")
                        .HasForeignKey("ApiUrlID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApiNinjectStudio.Domain.Entities.Role", "Role")
                        .WithMany("RolePermissionApiUrls")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApiNinjectStudio.Domain.Entities.User", b =>
                {
                    b.HasOne("WebApiNinjectStudio.Domain.Entities.PassWord", "PassWord")
                        .WithOne("User")
                        .HasForeignKey("WebApiNinjectStudio.Domain.Entities.User", "PassWordID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApiNinjectStudio.Domain.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
