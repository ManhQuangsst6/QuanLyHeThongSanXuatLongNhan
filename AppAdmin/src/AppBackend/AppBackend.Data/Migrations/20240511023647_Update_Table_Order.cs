using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AppBackend.Data.Migrations
{
    /// <inheritdoc />
    public partial class Update_Table_Order : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1f078569-82ae-4c94-a89c-cc689bc95b8b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "37de3635-4dc3-4072-b1aa-83fde0a307d2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fb35ba93-6f39-4865-a0c7-68d4ad07a027");

            migrationBuilder.AddColumn<int>(
                name: "IsDeleted",
                table: "Orders",
                type: "int",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "IsDeleted",
                table: "Orders");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1f078569-82ae-4c94-a89c-cc689bc95b8b", "1", "User", "User" },
                    { "37de3635-4dc3-4072-b1aa-83fde0a307d2", "3", "Manager", "Manager" },
                    { "fb35ba93-6f39-4865-a0c7-68d4ad07a027", "2", "Employee", "Employee" }
                });
        }
    }
}
