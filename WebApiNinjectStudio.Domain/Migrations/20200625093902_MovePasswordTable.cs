using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiNinjectStudio.Domain.Migrations
{
    public partial class MovePasswordTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_PassWords_PassWordID",
                table: "Users");

            migrationBuilder.DropTable(
                name: "PassWords");

            migrationBuilder.DropIndex(
                name: "IX_Users_PassWordID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PassWordID",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Users",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "Password",
                value: "M4jZrsPV2wNAeOH1YooKUdALx6Ek0DJaMf8yoiYI0Mc=");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "Password",
                value: "FOHqRDbYuVdIBvLS6r2YMVU4Yu7E54DJJJxrWGh5YZc=");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "PassWordID",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PassWords",
                columns: table => new
                {
                    PassWordID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassWords", x => x.PassWordID);
                });

            migrationBuilder.InsertData(
                table: "PassWords",
                columns: new[] { "PassWordID", "Created", "Password" },
                values: new object[] { 1, new DateTime(2020, 6, 18, 11, 21, 52, 70, DateTimeKind.Local).AddTicks(6149), "M4jZrsPV2wNAeOH1YooKUdALx6Ek0DJaMf8yoiYI0Mc=" });

            migrationBuilder.InsertData(
                table: "PassWords",
                columns: new[] { "PassWordID", "Created", "Password" },
                values: new object[] { 2, new DateTime(2020, 6, 18, 11, 21, 52, 87, DateTimeKind.Local).AddTicks(1268), "FOHqRDbYuVdIBvLS6r2YMVU4Yu7E54DJJJxrWGh5YZc=" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "PassWordID",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "PassWordID",
                value: 2);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PassWordID",
                table: "Users",
                column: "PassWordID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_PassWords_PassWordID",
                table: "Users",
                column: "PassWordID",
                principalTable: "PassWords",
                principalColumn: "PassWordID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
