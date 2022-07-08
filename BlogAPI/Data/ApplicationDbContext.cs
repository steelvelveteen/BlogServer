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
				Id = 1,
				FirstName = "Bradley",
				LastName = "Cooper",
				Address = null,
				Phone = null
			},
			new Customer
			{
				Id = 2,
				FirstName = "Sonoya",
				LastName = "Mizuno",
				Address = null,
				Phone = null
			},
			new Customer
			{
				Id = 3,
				FirstName = "John",
				LastName = "Wick",
				Address = null,
				Phone = null
			},
			new Customer
			{
				Id = 4,
				FirstName = "Mary Elise",
				LastName = "Windstead",
				Address = "154 Road, NYC 90454",
				Phone = null
			},
			new Customer
			{
				Id = 5,
				FirstName = "Scarlett",
				LastName = "Amancia",
				Address = null,
				Phone = null
			}
		);
	}
}