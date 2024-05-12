using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AppBackend.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_column_Table_RegisterDayLongan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2327c164-9bb9-4ad8-baa0-14684f92f4da");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8b670e08-1c04-4e75-b397-68382e768228");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "baaa985b-85a0-4008-953e-f7c08b74fd54");

            migrationBuilder.AddColumn<int>(
                name: "Icheck",
                table: "RegisterDayLongans",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Icheck",
                table: "RegisterDayLongans");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2327c164-9bb9-4ad8-baa0-14684f92f4da", "1", "User", "User" },
                    { "8b670e08-1c04-4e75-b397-68382e768228", "3", "Manager", "Manager" },
                    { "baaa985b-85a0-4008-953e-f7c08b74fd54", "2", "Employee", "Employee" }
                });
        }
    }
}
