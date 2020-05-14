using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiNinjectStudio.Domain.Migrations
{
    public partial class UpdateUserStructure01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_RolePermissions_RolePermissionID",
                table: "Users");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropIndex(
                name: "IX_Users_RolePermissionID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PassWord",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RolePermissionID",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PassWordID",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoleID",
                table: "Users",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.InsertData(
                table: "PassWords",
                columns: new[] { "PassWordID", "Created", "Password" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 5, 13, 20, 32, 45, 63, DateTimeKind.Local).AddTicks(6602), "HelloWorld" },
                    { 2, new DateTime(2020, 5, 13, 20, 32, 45, 73, DateTimeKind.Local).AddTicks(4722), "Abc123" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleID", "Description", "Name" },
                values: new object[,]
                {
                    { 1, null, "Administrator" },
                    { 2, null, "Guest" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "FirstName", "LastName", "PassWordID", "RoleID" },
                values: new object[] { "Kim", "Nielsen", 1, 1 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "FirstName", "LastName", "PassWordID", "RoleID" },
                values: new object[] { "Martin", "Jensen", 2, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Users_PassWordID",
                table: "Users",
                column: "PassWordID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleID",
                table: "Users",
                column: "RoleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_PassWords_PassWordID",
                table: "Users",
                column: "PassWordID",
                principalTable: "PassWords",
                principalColumn: "PassWordID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleID",
                table: "Users",
                column: "RoleID",
                principalTable: "Roles",
                principalColumn: "RoleID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_PassWords_PassWordID",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleID",
                table: "Users");

            migrationBuilder.DropTable(
                name: "PassWords");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Users_PassWordID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PassWordID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RoleID",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PassWord",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RolePermissionID",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    RolePermissionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AllowApiUrlID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.RolePermissionID);
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "RolePermissionID", "AllowApiUrlID", "RoleName" },
                values: new object[] { 1, "1,2,3,4", "Administrator" });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "RolePermissionID", "AllowApiUrlID", "RoleName" },
                values: new object[] { 2, "2", "Guest" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                columns: new[] { "Name", "PassWord", "RolePermissionID" },
                values: new object[] { "Kim", "Hello@World", 1 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                columns: new[] { "Name", "PassWord", "RolePermissionID" },
                values: new object[] { "Martin", "Abc@123", 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RolePermissionID",
                table: "Users",
                column: "RolePermissionID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_RolePermissions_RolePermissionID",
                table: "Users",
                column: "RolePermissionID",
                principalTable: "RolePermissions",
                principalColumn: "RolePermissionID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
