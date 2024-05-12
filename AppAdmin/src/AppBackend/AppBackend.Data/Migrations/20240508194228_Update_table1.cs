using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AppBackend.Data.Migrations
{
    /// <inheritdoc />
    public partial class Update_table1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Employees_EmployeeID",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Shipments_Order_OrderID",
                table: "Order_Shipments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

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
                name: "Isread",
                table: "RegisterRemainningLongans");

            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "Order");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.RenameIndex(
                name: "IX_Order_EmployeeID",
                table: "Orders",
                newName: "IX_Orders_EmployeeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1f078569-82ae-4c94-a89c-cc689bc95b8b", "1", "User", "User" },
                    { "37de3635-4dc3-4072-b1aa-83fde0a307d2", "3", "Manager", "Manager" },
                    { "fb35ba93-6f39-4865-a0c7-68d4ad07a027", "2", "Employee", "Employee" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Shipments_Orders_OrderID",
                table: "Order_Shipments",
                column: "OrderID",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Employees_EmployeeID",
                table: "Orders",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Shipments_Orders_OrderID",
                table: "Order_Shipments");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Employees_EmployeeID",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

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

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_EmployeeID",
                table: "Order",
                newName: "IX_Order_EmployeeID");

            migrationBuilder.AddColumn<int>(
                name: "Isread",
                table: "RegisterRemainningLongans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "OrderDate",
                table: "Order",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6341f54b-cb63-44f3-a843-27e8a0cf654a", "1", "User", "User" },
                    { "815d6574-dc59-4617-bc41-265b6408326e", "2", "Employee", "Employee" },
                    { "fe40f164-b7b1-45a4-b9bb-de973ceac405", "3", "Manager", "Manager" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Employees_EmployeeID",
                table: "Order",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Shipments_Order_OrderID",
                table: "Order_Shipments",
                column: "OrderID",
                principalTable: "Order",
                principalColumn: "Id");
        }
    }
}
