using System;
using Microsoft.EntityFrameworkCore;
using PMS.API.Seeders;
using PMS.Models;


namespace PMS.API;

public class PMSDbContext : DbContext
{
    public PMSDbContext(DbContextOptions<PMSDbContext> options) : base(options)
    {
        
    }

    public DbSet<PaintProduct> PaintProducts { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<PaintSeries> PaintSeries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ProductSeeder.SeedProductsData(modelBuilder);
    }
}
