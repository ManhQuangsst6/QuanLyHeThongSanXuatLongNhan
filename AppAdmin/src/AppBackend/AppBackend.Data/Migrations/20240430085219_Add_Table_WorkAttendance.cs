using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AppBackend.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_Table_WorkAttendance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "WorkAttendances",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmployeeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ListAmount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SumAmount = table.Column<int>(type: "int", nullable: true),
                    DateWork = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsSalary = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkAttendances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkAttendances_Employees_EmployeeID",
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
                    { "54c543bc-55d1-483f-8358-0b2bae8a3e7e", "3", "Manager", "Manager" },
                    { "5e82b663-9ff0-4e49-bf03-76481b1aeda1", "1", "User", "User" },
                    { "d35c7204-ce1a-417a-871e-0a550254fc7d", "2", "Employee", "Employee" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkAttendances_EmployeeID",
                table: "WorkAttendances",
                column: "EmployeeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkAttendances");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "54c543bc-55d1-483f-8358-0b2bae8a3e7e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e82b663-9ff0-4e49-bf03-76481b1aeda1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d35c7204-ce1a-417a-871e-0a550254fc7d");

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
    }
}
