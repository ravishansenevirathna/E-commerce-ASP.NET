using System.Text;
using EcommerceApi.Config;
using EcommerceApi.Models;
using EcommerceApi.Service;
using EcommerceApi.Service.Impl;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Configure controllers
builder.Services.AddControllers();

// Configure dbContext
builder.Services.AddDbContext<ProductContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add scoped services
builder.Services.AddScoped<IProductService, ProductServiceImpl>();
builder.Services.AddScoped<ICategoryService, CategoryServiceImpl>();
builder.Services.AddScoped<IUserService, UserServiceImpl>();
builder.Services.AddScoped<EmailSender>();

// Add Authentication using JWT
var jwtKey = builder.Configuration["Jwt:Key"]; // Fetch the secret key from appsettings.json
var jwtIssuer = builder.Configuration["Jwt:Issuer"]; // Fetch the issuer from appsettings.json
var key = Encoding.UTF8.GetBytes(jwtKey);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Use Authentication and Authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();