using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AppBackend.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_Column_In_WorkAttendance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<int>(
                name: "ComfirmAmount",
                table: "WorkAttendances",
                type: "int",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "ComfirmAmount",
                table: "WorkAttendances");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "54c543bc-55d1-483f-8358-0b2bae8a3e7e", "3", "Manager", "Manager" },
                    { "5e82b663-9ff0-4e49-bf03-76481b1aeda1", "1", "User", "User" },
                    { "d35c7204-ce1a-417a-871e-0a550254fc7d", "2", "Employee", "Employee" }
                });
        }
    }
}
