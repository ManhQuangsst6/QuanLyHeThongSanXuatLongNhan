using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AppBackend.Data.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "935ffa23-6c2a-4b95-92e7-6dd98cc65f43");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ca34e4a8-6cbc-4815-831a-b4573559b5f1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e4f8af17-dbaa-4fea-987a-e7a548aaba1f");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7f16eef8-85f7-498c-aeb9-3f2453ce3265", "1", "User", "User" },
                    { "db7c36c9-c29a-4073-8829-9bf05e7a9a16", "2", "Employee", "Employee" },
                    { "f092c3f9-394c-4228-80f9-226ce2c396d2", "3", "Manager", "Manager" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f16eef8-85f7-498c-aeb9-3f2453ce3265");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "db7c36c9-c29a-4073-8829-9bf05e7a9a16");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f092c3f9-394c-4228-80f9-226ce2c396d2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "935ffa23-6c2a-4b95-92e7-6dd98cc65f43", "1", "User", "User" },
                    { "ca34e4a8-6cbc-4815-831a-b4573559b5f1", "2", "Employee", "Employee" },
                    { "e4f8af17-dbaa-4fea-987a-e7a548aaba1f", "3", "Manager", "Manager" }
                });
        }
    }
}
