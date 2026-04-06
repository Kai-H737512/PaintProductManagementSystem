
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PMS.API.DTOs;
using PMS.API.Exceptions;
using PMS.API.Validators;
using PMS.DataAccess;
using PMS.Models;
using PMS.Respositories;
using PMS.Respositories.Interfaces;
using PMS.Services;
using PMS.Services.Interfaces;

namespace PMS.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        // Swagger with JWT support
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter your JWT token. Example: eyJhbGci..."
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                    },
                    Array.Empty<string>()
                }
            });
        });

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
            options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
        });

        builder.Services.AddDbContext<PMSDbContext>(
            options => options.UseSqlServer(builder.Configuration.GetConnectionString("PMS-SQLSERVER"))
        );

        builder.Services.AddIdentity<Users, IdentityRole>()
            .AddEntityFrameworkStores<PMSDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        builder.Services.AddScoped<IPaintProductRepository, PaintProductRepository>();

        // builder.Services.AddScoped<PaintProductRepository>();
        builder.Services.AddScoped<IOrderService, OrderService>();
        builder.Services.AddScoped<IPaintProductService, PaintProductService>();

        // builder.Services.AddScoped<IValidator<CreatePaintProductRequest>, CreatePaintProductRequestValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<CreatePaintProductRequestValidator>();
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddSingleton<GlobalExceptionHandler>();

        builder.Services.AddAutoMapper(cfg => { }, typeof(Program));

        // JWT Bearer authentication
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
            };
        });

        // builder.Services.AddDbContext<PMSDbContext>(
        //     options => options.UseNpgsql(builder.Configuration.GetConnectionString("PMS-PostgreSQL"))
        // );

        var app = builder.Build();

        // Seed roles
        using (var scope = app.Services.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            foreach (var role in new[] { "Admin", "User" })
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        app.UseExceptionHandler(
            errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var exceptionHandler = context.RequestServices.GetRequiredService<GlobalExceptionHandler>();
                    await exceptionHandler.HandleException(context);
                }
                );
            }
        );

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
