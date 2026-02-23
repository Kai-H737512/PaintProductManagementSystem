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

        modelBuilder.Entity<User>(entity => 
        {
            // 1. user constrains
            // PhoneNumber 约束：最长20，必填，在数据库里加唯一索引 (Unique)
            entity.Property(e => e.PhoneNumber).HasMaxLength(20).IsRequired();
            entity.HasIndex(e => e.PhoneNumber).IsUnique();
            // Email 约束：最长100，不是必填的(因为有?)，但如果有值则必须唯一
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.HasIndex(e => e.Email).IsUnique();
            // 地址与账户约束
            entity.Property(e => e.Address).HasMaxLength(100).IsRequired();
            entity.Property(e => e.DuluxAccountInfo).HasMaxLength(20);
            // CreatedAt 设置：如果在插入时没有给值，数据库默认生成当前时间
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()"); // SQL Server 写法
        });

        modelBuilder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId);
            
        modelBuilder.Entity<OrderPaintProduct>()
            .HasOne(op => op.Order)
            .WithMany(o => o.OrderPaintProducts)
            .HasForeignKey(op => op.OrderId);

        modelBuilder.Entity<OrderPaintProduct>()
            .HasOne(op => op.PaintProduct)
            .WithMany(p => p.OrderPaintProducts)
            .HasForeignKey(op => op.PaintProductId);

    }
}
