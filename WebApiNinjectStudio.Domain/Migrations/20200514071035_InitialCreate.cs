using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiNinjectStudio.Domain.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiUrls",
                columns: table => new
                {
                    ApiUrlID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApiUrlString = table.Column<string>(nullable: true),
                    ApiRequestMethod = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiUrls", x => x.ApiUrlID);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "PassWords",
                columns: table => new
                {
                    PassWordID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Password = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassWords", x => x.PassWordID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    ProductID = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => new { x.ProductID, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_ProductCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCategories_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    ProductImageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(nullable: true),
                    ProductID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.ProductImageId);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTags",
                columns: table => new
                {
                    ProductTagID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ProductID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTags", x => x.ProductTagID);
                    table.ForeignKey(
                        name: "FK_ProductTags_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissionApiUrls",
                columns: table => new
                {
                    ApiUrlID = table.Column<int>(nullable: false),
                    RoleID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissionApiUrls", x => new { x.RoleID, x.ApiUrlID });
                    table.ForeignKey(
                        name: "FK_RolePermissionApiUrls_ApiUrls_ApiUrlID",
                        column: x => x.ApiUrlID,
                        principalTable: "ApiUrls",
                        principalColumn: "ApiUrlID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissionApiUrls_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PassWordID = table.Column<int>(nullable: false),
                    RoleID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Users_PassWords_PassWordID",
                        column: x => x.PassWordID,
                        principalTable: "PassWords",
                        principalColumn: "PassWordID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ApiUrls",
                columns: new[] { "ApiUrlID", "ApiRequestMethod", "ApiUrlString" },
                values: new object[,]
                {
                    { 1, "Get", "/api/User" },
                    { 2, "GetUserID", "/api/User/GetUserID" },
                    { 3, "Get", "/api/product" },
                    { 4, "Post", "/api/product" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[,]
                {
                    { 8, "Indretning" },
                    { 7, "Have" },
                    { 6, "Opbevaring" },
                    { 5, "Spisestue" },
                    { 3, "Kontor" },
                    { 2, "Badeværelse" },
                    { 1, "Soveværelse" },
                    { 4, "Stue" }
                });

            migrationBuilder.InsertData(
                table: "PassWords",
                columns: new[] { "PassWordID", "Created", "Password" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 5, 14, 9, 10, 34, 388, DateTimeKind.Local).AddTicks(3310), "HelloWorld" },
                    { 2, new DateTime(2020, 5, 14, 9, 10, 34, 395, DateTimeKind.Local).AddTicks(9671), "Abc123" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Bremsehjul, Højdejusterbar, Nylonbase, Trinløs vippefunktion", "Kontorstol REGSTRUP sort/grå", 300m },
                    { 2, "Højdejusterbar", "Barstol KLARUP sort/krom", 250m }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleID", "Description", "Name" },
                values: new object[,]
                {
                    { 1, null, "Administrator" },
                    { 2, null, "Guest" }
                });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "ProductID", "CategoryId" },
                values: new object[,]
                {
                    { 1, 3 },
                    { 1, 5 },
                    { 2, 5 }
                });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "ProductImageId", "ProductID", "Url" },
                values: new object[,]
                {
                    { 1, 1, "jysk.dk/kontor/kontorstole/basic/kontorstol-regstrup-sort-graa" },
                    { 2, 2, "jysk.dk/spisestue/barborde-stole/barstol-klarup-sort-krom-0" }
                });

            migrationBuilder.InsertData(
                table: "ProductTags",
                columns: new[] { "ProductTagID", "Name", "ProductID" },
                values: new object[,]
                {
                    { 9, "polypropylen", 2 },
                    { 8, "metal", 2 },
                    { 7, "skum", 2 },
                    { 6, "kunstlæder", 2 },
                    { 4, "metal", 1 },
                    { 3, "skum", 1 },
                    { 2, "sort", 1 },
                    { 1, "kontorstol", 1 },
                    { 5, "krydsfiner", 1 }
                });

            migrationBuilder.InsertData(
                table: "RolePermissionApiUrls",
                columns: new[] { "RoleID", "ApiUrlID" },
                values: new object[,]
                {
                    { 2, 4 },
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 1, 4 },
                    { 2, 3 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Email", "FirstName", "LastName", "PassWordID", "PhoneNumber", "RoleID" },
                values: new object[,]
                {
                    { 1, "one@gmail.com", "Kim", "Nielsen", 1, null, 1 },
                    { 2, "two@gmail.com", "Martin", "Jensen", 2, null, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_CategoryId",
                table: "ProductCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductID",
                table: "ProductImages",
                column: "ProductID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductTags_ProductID",
                table: "ProductTags",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissionApiUrls_ApiUrlID",
                table: "RolePermissionApiUrls",
                column: "ApiUrlID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PassWordID",
                table: "Users",
                column: "PassWordID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleID",
                table: "Users",
                column: "RoleID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "ProductTags");

            migrationBuilder.DropTable(
                name: "RolePermissionApiUrls");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ApiUrls");

            migrationBuilder.DropTable(
                name: "PassWords");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
