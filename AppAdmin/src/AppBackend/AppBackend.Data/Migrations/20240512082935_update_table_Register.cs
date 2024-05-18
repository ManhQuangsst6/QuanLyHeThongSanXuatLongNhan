using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AppBackend.Data.Migrations
{
    /// <inheritdoc />
    public partial class update_table_Register : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Isread",
                table: "RegisterDayLongans");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "19f2dd1b-2e2e-4d53-b256-bc39b194c1fe", "3", "Manager", "Manager" },
                    { "9498e7ac-f539-4595-8563-1d08e69dda57", "2", "Employee", "Employee" },
                    { "b8274558-445e-41fc-9776-bcb14fcd2d2e", "1", "User", "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "19f2dd1b-2e2e-4d53-b256-bc39b194c1fe");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9498e7ac-f539-4595-8563-1d08e69dda57");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b8274558-445e-41fc-9776-bcb14fcd2d2e");

            migrationBuilder.AddColumn<int>(
                name: "Isread",
                table: "RegisterDayLongans",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
    }
}
