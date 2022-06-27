using BlogServer.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogServer.Data;
public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
	{
	}

	public DbSet<TestModel> TestModels { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		// base.OnModelCreating(modelBuilder);
		modelBuilder.Entity<TestModel>().HasData(
			new TestModel
			{
				Id = 10,
				TestModelName = "First name",
				TestModelOtherProperty = "Some other property name"
			},
			new TestModel
			{
				Id = 20,
				TestModelName = "Sec name",
				TestModelOtherProperty = "Some sec property name"
			},
			new TestModel
			{
				Id = 30,
				TestModelName = "Tres name",
				TestModelOtherProperty = "Some tres property name"
			}
		);

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