using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AppBackend.Data.Migrations
{
    /// <inheritdoc />
    public partial class Update_table_Attendance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "37ff3fd7-685c-4683-bf6d-9e90e26f7ee5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "751f1f45-deaa-493d-a873-17239b1eb05b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b6f82207-6c8f-43b7-bd61-4c7ba1b7effa");

            migrationBuilder.DropColumn(
                name: "DateWork",
                table: "WorkAttendances");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6838c0e2-9ead-424b-8a32-f112e50c512b", "3", "Manager", "Manager" },
                    { "895ad90a-03c8-41ad-bbe7-04d168dd8e56", "1", "User", "User" },
                    { "ddab71ea-34b7-4ff3-a6c4-7f5eb84d9768", "2", "Employee", "Employee" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6838c0e2-9ead-424b-8a32-f112e50c512b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "895ad90a-03c8-41ad-bbe7-04d168dd8e56");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ddab71ea-34b7-4ff3-a6c4-7f5eb84d9768");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateWork",
                table: "WorkAttendances",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "37ff3fd7-685c-4683-bf6d-9e90e26f7ee5", "2", "Employee", "Employee" },
                    { "751f1f45-deaa-493d-a873-17239b1eb05b", "3", "Manager", "Manager" },
                    { "b6f82207-6c8f-43b7-bd61-4c7ba1b7effa", "1", "User", "User" }
                });
        }
    }
}
