using System;
using Microsoft.EntityFrameworkCore;
using PMS.Models;

namespace PMS.DataAccess;

public class PMSDbContext : DbContext
{
    public PMSDbContext(DbContextOptions<PMSDbContext> options) : base(options)
    {
    }

    public DbSet<PaintProduct> PaintProducts { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>().HasKey(o => o.OrderId);
        modelBuilder.Entity<Order>().HasData(new List<Order>
        {

        });

        modelBuilder.Entity<PaintProduct>().Property(p => p.Description).HasMaxLength(50);
    }
}
