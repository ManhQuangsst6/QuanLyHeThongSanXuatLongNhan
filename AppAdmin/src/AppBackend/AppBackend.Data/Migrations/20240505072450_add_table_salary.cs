using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AppBackend.Data.Migrations
{
    /// <inheritdoc />
    public partial class add_table_salary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "74b42bbc-b5fe-413d-a3fe-c86351260334");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8b40d086-b556-4fe0-84dd-f89887b253e7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e01115de-da13-4d0c-ad02-7e0a0624b4c1");

            migrationBuilder.CreateTable(
                name: "Salaries",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmployeeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuarterYear = table.Column<int>(type: "int", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: true),
                    DateUp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SalaryMoney = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Salaries_Employees_EmployeeID",
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
                    { "72718dda-48d0-499f-9be3-fd22b5979b1c", "3", "Manager", "Manager" },
                    { "de759f63-f657-49a2-9f2e-69b65f8d8732", "1", "User", "User" },
                    { "df5315f3-f953-4943-93e4-4889834abfa9", "2", "Employee", "Employee" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Salaries_EmployeeID",
                table: "Salaries",
                column: "EmployeeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Salaries");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "72718dda-48d0-499f-9be3-fd22b5979b1c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "de759f63-f657-49a2-9f2e-69b65f8d8732");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "df5315f3-f953-4943-93e4-4889834abfa9");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "74b42bbc-b5fe-413d-a3fe-c86351260334", "1", "User", "User" },
                    { "8b40d086-b556-4fe0-84dd-f89887b253e7", "2", "Employee", "Employee" },
                    { "e01115de-da13-4d0c-ad02-7e0a0624b4c1", "3", "Manager", "Manager" }
                });
        }
    }
}
