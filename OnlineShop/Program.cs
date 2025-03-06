using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
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
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "OnlineShop");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/", () => "Hello World!");
app.Run();
