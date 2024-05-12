using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AppBackend.Data.Migrations
{
    /// <inheritdoc />
    public partial class remove_colum_Table_Salary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "17d4a4b5-5ad6-4f46-b86a-be1eb414c918");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "793f5f00-a713-4cf1-92f5-659b793d664a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7b9c3a09-97aa-48f6-b541-db259b2051b1");

            migrationBuilder.DropColumn(
                name: "DateUp",
                table: "Salaries");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5b8d861a-d1a4-4584-92f9-8be354909b30", "1", "User", "User" },
                    { "6ef899bd-9379-4fe8-8d87-084b61738aa9", "3", "Manager", "Manager" },
                    { "72d54d68-a592-424f-927f-c76efa387e61", "2", "Employee", "Employee" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5b8d861a-d1a4-4584-92f9-8be354909b30");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6ef899bd-9379-4fe8-8d87-084b61738aa9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "72d54d68-a592-424f-927f-c76efa387e61");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUp",
                table: "Salaries",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "17d4a4b5-5ad6-4f46-b86a-be1eb414c918", "3", "Manager", "Manager" },
                    { "793f5f00-a713-4cf1-92f5-659b793d664a", "1", "User", "User" },
                    { "7b9c3a09-97aa-48f6-b541-db259b2051b1", "2", "Employee", "Employee" }
                });
        }
    }
}
