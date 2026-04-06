
using FluentValidation;
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
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddSwaggerGen();

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
