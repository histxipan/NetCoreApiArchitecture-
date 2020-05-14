using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiNinjectStudio.Domain.Migrations
{
    public partial class UpdateUserStructure02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "PassWords",
                keyColumn: "PassWordID",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2020, 5, 13, 21, 9, 17, 971, DateTimeKind.Local).AddTicks(8508));

            migrationBuilder.UpdateData(
                table: "PassWords",
                keyColumn: "PassWordID",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2020, 5, 13, 21, 9, 17, 979, DateTimeKind.Local).AddTicks(618));

            migrationBuilder.InsertData(
                table: "RolePermissionApiUrls",
                columns: new[] { "RoleID", "ApiUrlID" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 1, 4 },
                    { 2, 3 },
                    { 2, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissionApiUrls_ApiUrlID",
                table: "RolePermissionApiUrls",
                column: "ApiUrlID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolePermissionApiUrls");

            migrationBuilder.UpdateData(
                table: "PassWords",
                keyColumn: "PassWordID",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2020, 5, 13, 20, 32, 45, 63, DateTimeKind.Local).AddTicks(6602));

            migrationBuilder.UpdateData(
                table: "PassWords",
                keyColumn: "PassWordID",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2020, 5, 13, 20, 32, 45, 73, DateTimeKind.Local).AddTicks(4722));
        }
    }
}
