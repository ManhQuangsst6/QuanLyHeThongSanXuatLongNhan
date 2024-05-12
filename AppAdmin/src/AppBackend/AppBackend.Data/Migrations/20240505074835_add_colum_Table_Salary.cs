using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AppBackend.Data.Migrations
{
    /// <inheritdoc />
    public partial class add_colum_Table_Salary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Salaries",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "Status",
                table: "Salaries");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "72718dda-48d0-499f-9be3-fd22b5979b1c", "3", "Manager", "Manager" },
                    { "de759f63-f657-49a2-9f2e-69b65f8d8732", "1", "User", "User" },
                    { "df5315f3-f953-4943-93e4-4889834abfa9", "2", "Employee", "Employee" }
                });
        }
    }
}
