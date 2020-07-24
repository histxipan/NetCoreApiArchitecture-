using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiNinjectStudio.Domain.Migrations
{
    public partial class UpdateApiUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApiUrlRegex",
                table: "ApiUrls",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ApiUrls",
                nullable: true);

            migrationBuilder.InsertData(
                table: "ApiUrls",
                columns: new[] { "ApiUrlID", "ApiRequestMethod", "ApiUrlRegex", "ApiUrlString", "Description", "IsDeleted" },
                values: new object[,]
                {
                    { 5, "Post", "^\\/api\\/v1\\/categories$", "/api/v1/categories", "Create category", false },
                    { 6, "Get", "^\\/api\\/v1\\/categories$", "/api/v1/categories", "Get category list", false },
                    { 7, "Get", "^\\/api\\/v1\\/categories\\/\\d+$", "/api/v1/categories/{categoryId}", "Get info about category by category id", false },
                    { 8, "Delete", "^\\/api\\/v1\\/categories\\/\\d+$", "/api/v1/categories/{categoryId}", "Delete category by identifier", false },
                    { 9, "Put", "^\\/api\\/v1\\/categories\\/\\d+$", "/api/v1/categories/{categoryId}", "Update category by identifier", false },
                    { 10, "Post", "^\\/api\\/v1\\/categories\\/\\d+\\/products$", "/api/v1/categories/{categoryId}/products", "Assign a product to the required category", false },
                    { 11, "Get", "^\\/api\\/v1\\/categories\\/\\d+\\/products(\\?.*)*$", "/api/v1/categories/{categoryId}/products", "Get products assigned to category", false },
                    { 12, "Put", "^\\/api\\/v1\\/categories\\/\\d+\\/products$", "/api/v1/categories/{categoryId}/products", "Assign a product to the required category", false },
                    { 13, "Delete", "^\\/api\\/v1\\/categories\\/\\d+\\/products\\/\\d+$", "/api/v1/categories/{categoryId}/products/{productId}", "Remove the product assignment from the category by category id and product id", false },
                    { 14, "Get", "^\\/api\\/v1\\/products(\\?.*)*$", "/api/v1/products", "Get product list", false },
                    { 15, "Post", "^\\/api\\/v1\\/products$", "/api/v1/products", "Create product", false },
                    { 16, "Get", "^\\/api\\/v1\\/products\\/\\d+$", "/api/v1/products/{productId}", "Get info about product by product id", false },
                    { 17, "Put", "^\\/api\\/v1\\/products\\/\\d+$", "/api/v1/products/{productId}", "Update the product by product id", false },
                    { 18, "Delete", "^\\/api\\/v1\\/products\\/\\d+$", "/api/v1/products/{productId}", "Remove the product by product id", false }
                });

            migrationBuilder.InsertData(
                table: "RolePermissionApiUrls",
                columns: new[] { "RoleID", "ApiUrlID", "IsDeleted" },
                values: new object[,]
                {
                    { 1, 5, false },
                    { 1, 6, false },
                    { 1, 7, false },
                    { 1, 8, false },
                    { 1, 9, false },
                    { 1, 10, false },
                    { 1, 11, false },
                    { 1, 12, false },
                    { 1, 13, false },
                    { 1, 14, false },
                    { 1, 15, false },
                    { 1, 16, false },
                    { 1, 17, false },
                    { 1, 18, false }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermissionApiUrls",
                keyColumns: new[] { "RoleID", "ApiUrlID" },
                keyValues: new object[] { 1, 5 });

            migrationBuilder.DeleteData(
                table: "RolePermissionApiUrls",
                keyColumns: new[] { "RoleID", "ApiUrlID" },
                keyValues: new object[] { 1, 6 });

            migrationBuilder.DeleteData(
                table: "RolePermissionApiUrls",
                keyColumns: new[] { "RoleID", "ApiUrlID" },
                keyValues: new object[] { 1, 7 });

            migrationBuilder.DeleteData(
                table: "RolePermissionApiUrls",
                keyColumns: new[] { "RoleID", "ApiUrlID" },
                keyValues: new object[] { 1, 8 });

            migrationBuilder.DeleteData(
                table: "RolePermissionApiUrls",
                keyColumns: new[] { "RoleID", "ApiUrlID" },
                keyValues: new object[] { 1, 9 });

            migrationBuilder.DeleteData(
                table: "RolePermissionApiUrls",
                keyColumns: new[] { "RoleID", "ApiUrlID" },
                keyValues: new object[] { 1, 10 });

            migrationBuilder.DeleteData(
                table: "RolePermissionApiUrls",
                keyColumns: new[] { "RoleID", "ApiUrlID" },
                keyValues: new object[] { 1, 11 });

            migrationBuilder.DeleteData(
                table: "RolePermissionApiUrls",
                keyColumns: new[] { "RoleID", "ApiUrlID" },
                keyValues: new object[] { 1, 12 });

            migrationBuilder.DeleteData(
                table: "RolePermissionApiUrls",
                keyColumns: new[] { "RoleID", "ApiUrlID" },
                keyValues: new object[] { 1, 13 });

            migrationBuilder.DeleteData(
                table: "RolePermissionApiUrls",
                keyColumns: new[] { "RoleID", "ApiUrlID" },
                keyValues: new object[] { 1, 14 });

            migrationBuilder.DeleteData(
                table: "RolePermissionApiUrls",
                keyColumns: new[] { "RoleID", "ApiUrlID" },
                keyValues: new object[] { 1, 15 });

            migrationBuilder.DeleteData(
                table: "RolePermissionApiUrls",
                keyColumns: new[] { "RoleID", "ApiUrlID" },
                keyValues: new object[] { 1, 16 });

            migrationBuilder.DeleteData(
                table: "RolePermissionApiUrls",
                keyColumns: new[] { "RoleID", "ApiUrlID" },
                keyValues: new object[] { 1, 17 });

            migrationBuilder.DeleteData(
                table: "RolePermissionApiUrls",
                keyColumns: new[] { "RoleID", "ApiUrlID" },
                keyValues: new object[] { 1, 18 });

            migrationBuilder.DeleteData(
                table: "ApiUrls",
                keyColumn: "ApiUrlID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ApiUrls",
                keyColumn: "ApiUrlID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ApiUrls",
                keyColumn: "ApiUrlID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ApiUrls",
                keyColumn: "ApiUrlID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ApiUrls",
                keyColumn: "ApiUrlID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ApiUrls",
                keyColumn: "ApiUrlID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ApiUrls",
                keyColumn: "ApiUrlID",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ApiUrls",
                keyColumn: "ApiUrlID",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ApiUrls",
                keyColumn: "ApiUrlID",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ApiUrls",
                keyColumn: "ApiUrlID",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "ApiUrls",
                keyColumn: "ApiUrlID",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "ApiUrls",
                keyColumn: "ApiUrlID",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "ApiUrls",
                keyColumn: "ApiUrlID",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "ApiUrls",
                keyColumn: "ApiUrlID",
                keyValue: 18);

            migrationBuilder.DropColumn(
                name: "ApiUrlRegex",
                table: "ApiUrls");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ApiUrls");
        }
    }
}
