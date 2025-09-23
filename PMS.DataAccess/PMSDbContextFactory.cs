using System;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

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

        var connnectionString = configuration.GetConnectionString("PMS-SQLSERVER");

        optionBuilder.UseSqlServer(connnectionString);
        return new PMSDbContext(optionBuilder.Options);
    }
}
