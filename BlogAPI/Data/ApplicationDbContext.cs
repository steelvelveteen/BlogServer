using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Data;
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
			},
			new Customer
			{
				Id = 99999,
				FirstName = "Mary Elise",
				LastName = "Windstead",
				Address = "154 Road, NYC 90454",
				Phone = null
			},
			new Customer
			{
				Id = 66777,
				FirstName = "Scarlett",
				LastName = "Amancia",
				Address = null,
				Phone = null
			}
		);
	}
}