using Microsoft.EntityFrameworkCore;
using BlogServer.Data;
using Serilog;
using BlogAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// Application DbContext configuration section
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlite(@"DataSource=test.db"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();
