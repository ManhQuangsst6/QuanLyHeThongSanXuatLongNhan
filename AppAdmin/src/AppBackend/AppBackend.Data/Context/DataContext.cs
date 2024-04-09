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
		protected override void OnModelCreating(ModelBuilder builder)
		{
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
