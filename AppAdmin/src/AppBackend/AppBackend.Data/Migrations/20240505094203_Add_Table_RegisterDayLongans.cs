using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AppBackend.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_Table_RegisterDayLongans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "RegisterDayLongans",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmployeeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisterDayLongans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegisterDayLongans_Employees_EmployeeID",
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
                    { "127e5e43-4658-4cdd-aa9d-4a102df96330", "1", "User", "User" },
                    { "38604f73-c0d4-40c0-89a6-7ffa83847902", "2", "Employee", "Employee" },
                    { "883a6ec4-eec1-46c2-8a6a-883cf377b890", "3", "Manager", "Manager" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegisterDayLongans_EmployeeID",
                table: "RegisterDayLongans",
                column: "EmployeeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegisterDayLongans");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "127e5e43-4658-4cdd-aa9d-4a102df96330");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "38604f73-c0d4-40c0-89a6-7ffa83847902");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "883a6ec4-eec1-46c2-8a6a-883cf377b890");

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
    }
}
