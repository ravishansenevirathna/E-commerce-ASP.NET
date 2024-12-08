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


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false, // Skip Issuer validation
            ValidateAudience = false, // Optional: Skip Audience validation if not needed
            ValidateLifetime = true, // Ensure the token has not expired
            ValidateIssuerSigningKey = true, // Always validate the signing key
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Use Authentication and Authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();