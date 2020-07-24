using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiNinjectStudio.Domain.Migrations
{
    public partial class UpdateApiurlTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ApiUrls",
                keyColumn: "ApiUrlID",
                keyValue: 2,
                column: "ApiRequestMethod",
                value: "Get");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ApiUrls",
                keyColumn: "ApiUrlID",
                keyValue: 2,
                column: "ApiRequestMethod",
                value: "GetUserID");
        }
    }
}
