using BlogAPI.Models;
using BlogServer.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogServer.Data;
public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
	{
	}

	public DbSet<Customer> Customers { get; set; } = null!;

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Customer>().HasData(
			new Customer
			{
				Id = 11111,
				FirstName = "Bradley",
				LastName = "Cooper",
				Address = null,
				Phone = null
			},
			new Customer
			{
				Id = 22222,
				FirstName = "Sonoya",
				LastName = "Mizuno",
				Address = null,
				Phone = null
			},
			new Customer
			{
				Id = 33333,
				FirstName = "John",
				LastName = "Wick",
				Address = null,
				Phone = null
			}
		);
	}
}