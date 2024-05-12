using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AppBackend.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_Table_db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "37930384-e2ff-43f1-8034-08a9b22b9a90");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "af85f5b7-2e4b-49ef-8e87-2c12e531f7ea");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b04e5db4-0f4f-4c18-b4f9-08a7cd8a8a05");

            migrationBuilder.RenameColumn(
                name: "Icheck",
                table: "RegisterDayLongans",
                newName: "Isread");

            migrationBuilder.AddColumn<int>(
                name: "Ischeck",
                table: "RegisterDayLongans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RegisterRemainningLongans",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmployeeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Ischeck = table.Column<int>(type: "int", nullable: false),
                    Isread = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisterRemainningLongans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegisterRemainningLongans_Employees_EmployeeID",
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
                    { "6eab4a2d-d5c6-4447-b3ba-3ec896d49396", "3", "Manager", "Manager" },
                    { "7074e9dc-81cf-4765-879c-8f192aaf0af2", "1", "User", "User" },
                    { "f0d9aa40-4d8b-4cb1-b3ee-46e027485e49", "2", "Employee", "Employee" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegisterRemainningLongans_EmployeeID",
                table: "RegisterRemainningLongans",
                column: "EmployeeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegisterRemainningLongans");

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

            migrationBuilder.DropColumn(
                name: "Ischeck",
                table: "RegisterDayLongans");

            migrationBuilder.RenameColumn(
                name: "Isread",
                table: "RegisterDayLongans",
                newName: "Icheck");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "37930384-e2ff-43f1-8034-08a9b22b9a90", "1", "User", "User" },
                    { "af85f5b7-2e4b-49ef-8e87-2c12e531f7ea", "2", "Employee", "Employee" },
                    { "b04e5db4-0f4f-4c18-b4f9-08a7cd8a8a05", "3", "Manager", "Manager" }
                });
        }
    }
}
