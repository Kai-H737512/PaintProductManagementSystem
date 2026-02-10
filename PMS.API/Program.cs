
namespace PMS.API;

using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<PMSDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("PMS-SQLSERVER"));
        });

        // builder.Services.AddDbContext<PMSDbContext>(options =>
        // {
        //     options.UseNpgsql(builder.Configuration.GetConnectionString("PMS-PostgresSQL"));
        // });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
