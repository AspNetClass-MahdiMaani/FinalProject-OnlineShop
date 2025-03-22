using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OnlineShop.ApplicationServices.Contracts;
using OnlineShop.ApplicationServices.Services;
using OnlineShop.Models;
using OnlineShop.Models.Services.Contracts;
using OnlineShop.Models.Services.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped<IPersonRepository,PersonRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<FinalProjectDbContext>();

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IProductService, ProductService>();

#region [- Connectionstring() -]

builder.Services.AddDbContext<FinalProjectDbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("Default")
        ));

#endregion

#region [- Swagger() -]

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "OnlineShop",
        Version = "v1",
        Description = "API for managing OnlineShop"
    });
});

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "OnlineShop v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
