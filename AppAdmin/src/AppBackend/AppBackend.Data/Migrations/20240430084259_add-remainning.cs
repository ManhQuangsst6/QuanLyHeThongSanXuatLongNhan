using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AppBackend.Data.Migrations
{
    /// <inheritdoc />
    public partial class addremainning : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<double>(
                name: "Remainning",
                table: "Shipments",
                type: "float",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0be01c2d-330e-4db8-89ef-2a74b235fde0", "3", "Manager", "Manager" },
                    { "f9a997bd-9edd-4374-9acd-a47726d531b4", "2", "Employee", "Employee" },
                    { "faec96f9-679c-4f3e-9446-a71a529098e2", "1", "User", "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0be01c2d-330e-4db8-89ef-2a74b235fde0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f9a997bd-9edd-4374-9acd-a47726d531b4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "faec96f9-679c-4f3e-9446-a71a529098e2");

            migrationBuilder.DropColumn(
                name: "Remainning",
                table: "Shipments");

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
    }
}
