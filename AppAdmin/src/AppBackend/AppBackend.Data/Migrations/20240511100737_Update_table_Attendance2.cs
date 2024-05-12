using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AppBackend.Data.Migrations
{
    /// <inheritdoc />
    public partial class Update_table_Attendance2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "IsSalary",
                table: "WorkAttendances",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "35c2ce90-ec4a-4910-b5cf-e05458198884", "1", "User", "User" },
                    { "56baf446-d045-48aa-9df7-7f7da465a3fa", "3", "Manager", "Manager" },
                    { "a6dfb1a7-aed5-425c-914e-907ee1b78ba1", "2", "Employee", "Employee" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "35c2ce90-ec4a-4910-b5cf-e05458198884");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "56baf446-d045-48aa-9df7-7f7da465a3fa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a6dfb1a7-aed5-425c-914e-907ee1b78ba1");

            migrationBuilder.AlterColumn<int>(
                name: "IsSalary",
                table: "WorkAttendances",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
    }
}
