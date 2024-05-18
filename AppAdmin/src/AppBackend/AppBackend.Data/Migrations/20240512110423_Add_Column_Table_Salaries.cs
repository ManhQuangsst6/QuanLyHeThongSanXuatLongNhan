using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AppBackend.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_Column_Table_Salaries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "SumAmount",
                table: "Salaries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0b0f43a8-82e3-40be-87e8-011148b99929", "1", "User", "User" },
                    { "207fa111-2112-4ecd-9285-f4d8b7a68cfc", "2", "Employee", "Employee" },
                    { "d702e3cb-80f7-4a5c-8d3a-76af396419b6", "3", "Manager", "Manager" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0b0f43a8-82e3-40be-87e8-011148b99929");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "207fa111-2112-4ecd-9285-f4d8b7a68cfc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d702e3cb-80f7-4a5c-8d3a-76af396419b6");

            migrationBuilder.DropColumn(
                name: "SumAmount",
                table: "Salaries");

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
    }
}
