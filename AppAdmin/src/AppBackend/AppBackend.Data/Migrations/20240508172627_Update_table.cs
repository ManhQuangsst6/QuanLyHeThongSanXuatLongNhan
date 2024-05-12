using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AppBackend.Data.Migrations
{
    /// <inheritdoc />
    public partial class Update_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "05f5645b-6da2-4beb-9965-2580e59247a0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a50eeea6-8d98-415e-8baa-94c581c358be");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b64df5ad-0e70-474c-a0dc-670da2e20716");

            migrationBuilder.AddColumn<int>(
                name: "IsComfirm",
                table: "ComfirmLongans",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6341f54b-cb63-44f3-a843-27e8a0cf654a", "1", "User", "User" },
                    { "815d6574-dc59-4617-bc41-265b6408326e", "2", "Employee", "Employee" },
                    { "fe40f164-b7b1-45a4-b9bb-de973ceac405", "3", "Manager", "Manager" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6341f54b-cb63-44f3-a843-27e8a0cf654a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "815d6574-dc59-4617-bc41-265b6408326e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fe40f164-b7b1-45a4-b9bb-de973ceac405");

            migrationBuilder.DropColumn(
                name: "IsComfirm",
                table: "ComfirmLongans");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "05f5645b-6da2-4beb-9965-2580e59247a0", "2", "Employee", "Employee" },
                    { "a50eeea6-8d98-415e-8baa-94c581c358be", "3", "Manager", "Manager" },
                    { "b64df5ad-0e70-474c-a0dc-670da2e20716", "1", "User", "User" }
                });
        }
    }
}
