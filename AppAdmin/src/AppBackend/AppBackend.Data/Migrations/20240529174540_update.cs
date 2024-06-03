using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AppBackend.Data.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "658b7e52-433f-4242-acb6-5d7517347ff7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "94a0710a-8ec6-4ade-bb42-24a7744cf403");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ee797ae4-9de5-41b3-b154-37e08e343a15");

            migrationBuilder.RenameColumn(
                name: "link",
                table: "Notifications",
                newName: "Link");

            migrationBuilder.RenameColumn(
                name: "isRead",
                table: "Notifications",
                newName: "IsRead");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "70c6f42c-18c4-48fe-81e3-2663fa0f12ec", "1", "User", "User" },
                    { "b3af20c0-108c-423b-b1b7-a5455f64e472", "2", "Employee", "Employee" },
                    { "bbbe8c6a-d8fe-4c2e-8015-fca9f4cd4a06", "3", "Manager", "Manager" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "70c6f42c-18c4-48fe-81e3-2663fa0f12ec");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b3af20c0-108c-423b-b1b7-a5455f64e472");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bbbe8c6a-d8fe-4c2e-8015-fca9f4cd4a06");

            migrationBuilder.RenameColumn(
                name: "Link",
                table: "Notifications",
                newName: "link");

            migrationBuilder.RenameColumn(
                name: "IsRead",
                table: "Notifications",
                newName: "isRead");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "658b7e52-433f-4242-acb6-5d7517347ff7", "1", "User", "User" },
                    { "94a0710a-8ec6-4ade-bb42-24a7744cf403", "2", "Employee", "Employee" },
                    { "ee797ae4-9de5-41b3-b154-37e08e343a15", "3", "Manager", "Manager" }
                });
        }
    }
}
