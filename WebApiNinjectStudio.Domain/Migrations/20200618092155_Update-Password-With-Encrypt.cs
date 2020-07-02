using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiNinjectStudio.Domain.Migrations
{
    public partial class UpdatePasswordWithEncrypt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PassWords",
                keyColumn: "PassWordID",
                keyValue: 1,
                columns: new[] { "Created", "Password" },
                values: new object[] { new DateTime(2020, 6, 18, 11, 21, 52, 70, DateTimeKind.Local).AddTicks(6149), "M4jZrsPV2wNAeOH1YooKUdALx6Ek0DJaMf8yoiYI0Mc=" });

            migrationBuilder.UpdateData(
                table: "PassWords",
                keyColumn: "PassWordID",
                keyValue: 2,
                columns: new[] { "Created", "Password" },
                values: new object[] { new DateTime(2020, 6, 18, 11, 21, 52, 87, DateTimeKind.Local).AddTicks(1268), "FOHqRDbYuVdIBvLS6r2YMVU4Yu7E54DJJJxrWGh5YZc=" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PassWords",
                keyColumn: "PassWordID",
                keyValue: 1,
                columns: new[] { "Created", "Password" },
                values: new object[] { new DateTime(2020, 5, 14, 9, 10, 34, 388, DateTimeKind.Local).AddTicks(3310), "HelloWorld" });

            migrationBuilder.UpdateData(
                table: "PassWords",
                keyColumn: "PassWordID",
                keyValue: 2,
                columns: new[] { "Created", "Password" },
                values: new object[] { new DateTime(2020, 5, 14, 9, 10, 34, 395, DateTimeKind.Local).AddTicks(9671), "Abc123" });
        }
    }
}
