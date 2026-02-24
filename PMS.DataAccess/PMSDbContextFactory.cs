using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using PMS.DataAcces;

namespace PMS.DataAccess;

public class PMSDbContextFactory : IDesignTimeDbContextFactory<PMSDbContext>
{
    public PMSDbContext CreateDbContext(string[] args)
    {
        var optionBuilder = new DbContextOptionsBuilder<PMSDbContext>();

        var configuration = new ConfigurationBuilder()
        .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "PMS.API"))
        .AddJsonFile("appsettings.json", optional: false)
        .AddJsonFile("appsettings.Development.json", optional: false)
        .Build();

        var connectionString = configuration.GetConnectionString("PMS-SQLSERVER");

        optionBuilder.UseSqlServer(connectionString);
        return new PMSDbContext(optionBuilder.Options);

    }
}
