using AppBackend.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppBackend.Data.Context
{
	public class DataContext : IdentityDbContext
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{
		}
		public DbSet<Ingredient> Ingredients { set; get; }
		public DbSet<PurchaseOrder> PurchaseOrders { set; get; }
		public DbSet<Notification> Notifications { set; get; }
		//public DbSet<Position> Positions { set; get; }
		public DbSet<Employee> Employees { set; get; }
		public DbSet<Category> No { set; get; }
		public DbSet<Shipment> Shipments { set; get; }
		public DbSet<Event> Events { set; get; }
		public DbSet<Order_Shipment> Order_Shipments { set; get; }
		public DbSet<Order> Orders { set; get; }
		public DbSet<WorkAttendance> WorkAttendances { set; get; }
		public DbSet<Salary> Salaries { set; get; }
		public DbSet<RegisterDayLongan> RegisterDayLongans { set; get; }
		public DbSet<RegisterRemainningLongan> RegisterRemainningLongans { set; get; }
		public DbSet<ComfirmLongan> ComfirmLongans { set; get; }
		protected override void OnModelCreating(ModelBuilder builder)
		{

			builder.Entity<Order_Shipment>()
		   .HasKey(sc => new { sc.OrderID, sc.ShipmentID });

			builder.Entity<Order_Shipment>()
				.HasOne(sc => sc.Order)
				.WithMany(s => s.Order_Shipments)
				.HasForeignKey(sc => sc.OrderID)
				.OnDelete(DeleteBehavior.ClientSetNull); ;

			builder.Entity<Order_Shipment>()
				.HasOne(sc => sc.Shipment)
				.WithMany(c => c.Order_Shipments)
				.HasForeignKey(sc => sc.ShipmentID)
				.OnDelete(DeleteBehavior.ClientSetNull);
			base.OnModelCreating(builder);
			SeeRole(builder);
		}
		public void SeeRole(ModelBuilder builder)
		{
			builder.Entity<IdentityRole>().HasData(
				new IdentityRole() { Name = "User", ConcurrencyStamp = "1", NormalizedName = "User" },
					new IdentityRole() { Name = "Employee", ConcurrencyStamp = "2", NormalizedName = "Employee" },
						new IdentityRole() { Name = "Manager", ConcurrencyStamp = "3", NormalizedName = "Manager" }
			);
		}


	}
}
