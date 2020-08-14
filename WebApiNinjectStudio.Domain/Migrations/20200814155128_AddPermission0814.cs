using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiNinjectStudio.Domain.Migrations
{
    public partial class AddPermission0814 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ApiUrls",
                columns: new[] { "ApiUrlID", "ApiRequestMethod", "ApiUrlRegex", "ApiUrlString", "Description", "IsDeleted" },
                values: new object[] { 19, "Get", "^\\/api\\/v1\\/integrations\\/customer\\/userid$", "/api/v1/integrations/customer/userid", "Get id of current user.", false });

            migrationBuilder.InsertData(
                table: "RolePermissionApiUrls",
                columns: new[] { "RoleID", "ApiUrlID", "IsDeleted" },
                values: new object[] { 1, 19, false });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermissionApiUrls",
                keyColumns: new[] { "RoleID", "ApiUrlID" },
                keyValues: new object[] { 1, 19 });

            migrationBuilder.DeleteData(
                table: "ApiUrls",
                keyColumn: "ApiUrlID",
                keyValue: 19);
        }
    }
}
