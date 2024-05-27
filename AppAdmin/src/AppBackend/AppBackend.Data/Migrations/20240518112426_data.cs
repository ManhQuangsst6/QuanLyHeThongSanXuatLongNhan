using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AppBackend.Data.Migrations
{
    /// <inheritdoc />
    public partial class data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Employees_EmployeeId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_EmployeeId",
                table: "Notifications");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "31205e0e-cc78-4f21-979e-02afd81ee401");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "46459509-3dbf-454b-90ec-274e4afcc1a9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6e75fe0c-81f3-48f9-aaf7-ad57ca3d4bdc");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Notifications");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "Notifications",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "31205e0e-cc78-4f21-979e-02afd81ee401", "2", "Employee", "Employee" },
                    { "46459509-3dbf-454b-90ec-274e4afcc1a9", "1", "User", "User" },
                    { "6e75fe0c-81f3-48f9-aaf7-ad57ca3d4bdc", "3", "Manager", "Manager" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_EmployeeId",
                table: "Notifications",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Employees_EmployeeId",
                table: "Notifications",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
