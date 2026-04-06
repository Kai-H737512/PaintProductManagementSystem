
using FluentValidation;
using Scalar.AspNetCore;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

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
            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
