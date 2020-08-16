using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SixBDigital.Web.Migrations
{
    public partial class addapprovedcolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "Bookings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "Password" },
                values: new object[] { new DateTime(2020, 8, 16, 0, 51, 14, 485, DateTimeKind.Local).AddTicks(8561), "$2a$11$vyc7nYKrm39rxLlzpvrHq.JLfd34Demfaze2MJs.zDUfjIVXuDyF." });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approved",
                table: "Bookings");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "Password" },
                values: new object[] { new DateTime(2020, 8, 15, 21, 31, 20, 673, DateTimeKind.Local).AddTicks(8725), "$2a$11$p2pJTjY7v6MSF4h6MZQzhuiyeYAsKQ.LBqaWO1MosArHbtzIkxoie" });
        }
    }
}
