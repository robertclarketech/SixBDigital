using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SixBDigital.Web.Migrations
{
    public partial class adddefaultadminuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateCreated", "DateEdited", "Password", "Username" },
                values: new object[] { 1, new DateTime(2020, 8, 15, 21, 31, 20, 673, DateTimeKind.Local).AddTicks(8725), null, "$2a$11$p2pJTjY7v6MSF4h6MZQzhuiyeYAsKQ.LBqaWO1MosArHbtzIkxoie", "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
