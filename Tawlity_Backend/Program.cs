using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using System.Security.Claims;
using System.Text;
using Tawlity.Core.Enums;
using Tawlity_Backend.Data;
using Tawlity_Backend.Dtos;
using Tawlity_Backend.Models;
using Tawlity_Backend.Repositories.Interface;
using Tawlity_Backend.Repositories.Repositories;
using Tawlity_Backend.Services;
using Tawlity_Backend.Services.Interface;
using Tawlity_Backend.Services.IService;
using Tawlity_Backend.Services.Repo;
using Tawlity_Backend.Services.Service;

var builder = WebApplication.CreateBuilder(args);
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]!);
var ValidAudience = builder.Configuration["Jwt:Audience"];
var ValidIssuer = builder.Configuration["Jwt:Issuer"];



// Enable CORS

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()  // Allows all origins (You can restrict this to specific domains if needed)
               .AllowAnyMethod()  // Allows all HTTP methods (GET, POST, PUT, DELETE, etc.)
               .AllowAnyHeader(); // Allows all headers
    });
});



// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(x =>
    x.UseSqlServer(builder.Configuration.GetConnectionString("connection")));


builder.Services.AddScoped<Login_IRepo, Login_Repo>();
builder.Services.AddScoped<Login_IService, Login_Service>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IMenuService, MenuService>();


builder.Services.AddSingleton<EmailService>();
builder.Services.Configure<EmailService>(builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("RestaurantOwner", policy => policy.RequireRole("RestaurantOwner"));
    options.AddPolicy("AdminOrOwner", policy =>
        policy.RequireAssertion(context =>
        {
            var roleClaim = context.User.FindFirst(ClaimTypes.Role);
            if (roleClaim == null) return false;
            return roleClaim.Value == "Admin" || roleClaim.Value == "RestaurantOwner";
        }));
});

// Add Authentication with JWT Bearer   
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            RoleClaimType = ClaimTypes.Role,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization();


builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter 'Bearer' followed by a space and your token. Example: Bearer <token>"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        {
            {
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference
                    {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        });
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOrRestaurantOwner", policy =>
        policy.RequireAssertion(context =>
        {
            var roleClaim = context.User.FindFirst(ClaimTypes.Role);
            if (roleClaim == null) return false;

            return roleClaim.Value == Employee_Role.Admin.ToString() ||
                   roleClaim.Value == Employee_Role.RestaurantOwner.ToString();
        }));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

// Enable CORS
app.UseCors("AllowAllOrigins");  // Apply the CORS policy

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();



// eTicket logoin data
//admin@etickets.com
//Coding@1234?