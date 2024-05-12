using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AppBackend.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6eab4a2d-d5c6-4447-b3ba-3ec896d49396");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7074e9dc-81cf-4765-879c-8f192aaf0af2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f0d9aa40-4d8b-4cb1-b3ee-46e027485e49");

            migrationBuilder.AddColumn<double>(
                name: "NumAmount",
                table: "Order_Shipments",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "ComfirmLongans",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmployeeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComfirmLongans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComfirmLongans_Employees_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "05f5645b-6da2-4beb-9965-2580e59247a0", "2", "Employee", "Employee" },
                    { "a50eeea6-8d98-415e-8baa-94c581c358be", "3", "Manager", "Manager" },
                    { "b64df5ad-0e70-474c-a0dc-670da2e20716", "1", "User", "User" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComfirmLongans_EmployeeID",
                table: "ComfirmLongans",
                column: "EmployeeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComfirmLongans");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "05f5645b-6da2-4beb-9965-2580e59247a0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a50eeea6-8d98-415e-8baa-94c581c358be");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b64df5ad-0e70-474c-a0dc-670da2e20716");

            migrationBuilder.DropColumn(
                name: "NumAmount",
                table: "Order_Shipments");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6eab4a2d-d5c6-4447-b3ba-3ec896d49396", "3", "Manager", "Manager" },
                    { "7074e9dc-81cf-4765-879c-8f192aaf0af2", "1", "User", "User" },
                    { "f0d9aa40-4d8b-4cb1-b3ee-46e027485e49", "2", "Employee", "Employee" }
                });
        }
    }
}
