﻿// <auto-generated />
using System;
using AppBackend.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AppBackend.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240511100652_Update_table_Attendance")]
    partial class Update_table_Attendance
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AppBackend.Data.Models.Category", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset?>("Created")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("LastModified")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("RetailPrice")
                        .HasColumnType("decimal(18,3)");

                    b.Property<decimal>("WholesalePrice")
                        .HasColumnType("decimal(18,3)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("AppBackend.Data.Models.ComfirmLongan", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<DateTimeOffset?>("Created")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("IsComfirm")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("LastModified")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeID");

                    b.ToTable("ComfirmLongans");
                });

            modelBuilder.Entity("AppBackend.Data.Models.Employee", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset?>("Created")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IsDeleted")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("LastModified")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("AppBackend.Data.Models.Event", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset?>("Created")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("DateStart")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Expense")
                        .HasColumnType("decimal(18,3)");

                    b.Property<DateTimeOffset?>("LastModified")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeID");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("AppBackend.Data.Models.Ingredient", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset?>("Created")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("LastModified")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Measure")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("AppBackend.Data.Models.Order", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("CategoryLongan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("Created")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Customer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("IsDeleted")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("LastModified")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,3)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("AppBackend.Data.Models.Order_Shipment", b =>
                {
                    b.Property<string>("OrderID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ShipmentID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Money")
                        .HasColumnType("decimal(18,3)");

                    b.Property<double>("NumAmount")
                        .HasColumnType("float");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,3)");

                    b.HasKey("OrderID", "ShipmentID");

                    b.HasIndex("ShipmentID");

                    b.ToTable("Order_Shipments");
                });

            modelBuilder.Entity("AppBackend.Data.Models.PurchaseOrder", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("Created")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("IngredientID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("IsDelete")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("LastModified")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("OrderDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,3)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeID");

                    b.HasIndex("IngredientID");

                    b.ToTable("PurchaseOrders");
                });

            modelBuilder.Entity("AppBackend.Data.Models.RegisterDayLongan", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("Created")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Ischeck")
                        .HasColumnType("int");

                    b.Property<int>("Isread")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("LastModified")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeID");

                    b.ToTable("RegisterDayLongans");
                });

            modelBuilder.Entity("AppBackend.Data.Models.RegisterRemainningLongan", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("Created")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Ischeck")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("LastModified")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeID");

                    b.ToTable("RegisterRemainningLongans");
                });

            modelBuilder.Entity("AppBackend.Data.Models.Salary", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset?>("Created")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset?>("LastModified")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("QuarterYear")
                        .HasColumnType("int");

                    b.Property<decimal>("SalaryMoney")
                        .HasColumnType("decimal(18,3)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int?>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeID");

                    b.ToTable("Salaries");
                });

            modelBuilder.Entity("AppBackend.Data.Models.Shipment", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double?>("Amount")
                        .HasColumnType("float");

                    b.Property<string>("CategoryID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset?>("Created")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("DateFrom")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateTo")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateUp")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("EmployeeID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("IsDelete")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("LastModified")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Remainning")
                        .HasColumnType("float");

                    b.Property<string>("ShipmentCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryID");

                    b.HasIndex("EmployeeID");

                    b.ToTable("Shipments");
                });

            modelBuilder.Entity("AppBackend.Data.Models.WorkAttendance", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("ComfirmAmount")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("Created")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("IsSalary")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("LastModified")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ListAmount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SumAmount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeID");

                    b.ToTable("WorkAttendances");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "895ad90a-03c8-41ad-bbe7-04d168dd8e56",
                            ConcurrencyStamp = "1",
                            Name = "User",
                            NormalizedName = "User"
                        },
                        new
                        {
                            Id = "ddab71ea-34b7-4ff3-a6c4-7f5eb84d9768",
                            ConcurrencyStamp = "2",
                            Name = "Employee",
                            NormalizedName = "Employee"
                        },
                        new
                        {
                            Id = "6838c0e2-9ead-424b-8a32-f112e50c512b",
                            ConcurrencyStamp = "3",
                            Name = "Manager",
                            NormalizedName = "Manager"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("AppBackend.Data.Models.ComfirmLongan", b =>
                {
                    b.HasOne("AppBackend.Data.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("AppBackend.Data.Models.Event", b =>
                {
                    b.HasOne("AppBackend.Data.Models.Employee", "Employee")
                        .WithMany("Event")
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("AppBackend.Data.Models.Order", b =>
                {
                    b.HasOne("AppBackend.Data.Models.Employee", "Employee")
                        .WithMany("Order")
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("AppBackend.Data.Models.Order_Shipment", b =>
                {
                    b.HasOne("AppBackend.Data.Models.Order", "Order")
                        .WithMany("Order_Shipments")
                        .HasForeignKey("OrderID")
                        .IsRequired();

                    b.HasOne("AppBackend.Data.Models.Shipment", "Shipment")
                        .WithMany("Order_Shipments")
                        .HasForeignKey("ShipmentID")
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Shipment");
                });

            modelBuilder.Entity("AppBackend.Data.Models.PurchaseOrder", b =>
                {
                    b.HasOne("AppBackend.Data.Models.Employee", "Employee")
                        .WithMany("PurchaseOrders")
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppBackend.Data.Models.Ingredient", "Ingredient")
                        .WithMany("PurchaseOrders")
                        .HasForeignKey("IngredientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Ingredient");
                });

            modelBuilder.Entity("AppBackend.Data.Models.RegisterDayLongan", b =>
                {
                    b.HasOne("AppBackend.Data.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("AppBackend.Data.Models.RegisterRemainningLongan", b =>
                {
                    b.HasOne("AppBackend.Data.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("AppBackend.Data.Models.Salary", b =>
                {
                    b.HasOne("AppBackend.Data.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("AppBackend.Data.Models.Shipment", b =>
                {
                    b.HasOne("AppBackend.Data.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppBackend.Data.Models.Employee", "Employee")
                        .WithMany("Shipment")
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("AppBackend.Data.Models.WorkAttendance", b =>
                {
                    b.HasOne("AppBackend.Data.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AppBackend.Data.Models.Employee", b =>
                {
                    b.Navigation("Event");

                    b.Navigation("Order");

                    b.Navigation("PurchaseOrders");

                    b.Navigation("Shipment");
                });

            modelBuilder.Entity("AppBackend.Data.Models.Ingredient", b =>
                {
                    b.Navigation("PurchaseOrders");
                });

            modelBuilder.Entity("AppBackend.Data.Models.Order", b =>
                {
                    b.Navigation("Order_Shipments");
                });

            modelBuilder.Entity("AppBackend.Data.Models.Shipment", b =>
                {
                    b.Navigation("Order_Shipments");
                });
#pragma warning restore 612, 618
        }
    }
}
