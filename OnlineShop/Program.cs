using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;
using OnlineShop.Models.Services.Contracts;
using OnlineShop.Models.Services.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<IProductRepository,ProductRepository>();

builder.Services.AddDbContext<DataBaseContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("Default")
        ));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
