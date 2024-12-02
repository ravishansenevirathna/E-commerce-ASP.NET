using EcommerceApi.Models;
using EcommerceApi.Service;
using EcommerceApi.Service.Impl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

//configure controllers
builder.Services.AddControllers();

//configure dbContext
builder.Services.AddDbContext<ProductContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProductService, ProductServiceImpl>();


var app = builder.Build();

app.UseAuthentication();
app.MapControllers();


app.Run();


