using Microsoft.EntityFrameworkCore;
using BlogAPI.Data;
using Serilog;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// Application DbContext configuration section
builder.Services.AddDbContext<ApplicationDbContext>(
	options => options.UseSqlite(@"DataSource=test.db"));

builder.Services.AddEndpointsApiExplorer();

// Configuration for Swagger
builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new OpenApiInfo
	{
		Version = "v1",
		Title = "Blog API",
		Description = "Although it's named as a Blog API it's primary purpose is to serve as a full server base for teaching and for my own personal use in future projects",
		TermsOfService = new Uri("https://example.com/terms")
	});

	var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

});

// Serilog
builder.Host.UseSerilog((ctx, lc) =>
	lc.WriteTo.Console()
);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;

	var context = services.GetRequiredService<ApplicationDbContext>();
	if (context.Database.GetPendingMigrations().Any())
	{
		context.Database.Migrate();
	}
}

app.Run();
