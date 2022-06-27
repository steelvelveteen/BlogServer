using BlogServer.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogServer.Data;
public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
	{
	}

	public DbSet<TestModel> TestModels { get; set; } = null!;

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
	}
}