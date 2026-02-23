using System;
using Microsoft.EntityFrameworkCore;
using PMS.Models;

namespace PMS.API.Seeders;

public static class ProductSeeder
{
    public static void SeedProductsData(ModelBuilder modelBuilder)
    {
         modelBuilder.Entity<PaintProduct>().HasData(
            new PaintProduct
            {
                Id = 1,
                PaintProductName = "Product 1",
                Description = "This is product 1",
                DuluxId = new Guid()
            },
            new PaintProduct
            {
                Id = 2,
                PaintProductName = "Product 2",
                Description = "This is product 2",
                DuluxId = new Guid()
            }
        );
    }

    public static void SeedAsync(PMSDbContext dbContext)
    {
        var product = new List<PaintProduct>()
        {
            new PaintProduct
            {
                Id = 1,
                PaintProductName = "Product 1",
                Description = "This is product 1",
                DuluxId = new Guid()
            },
            new PaintProduct
            {
                Id = 2,
                PaintProductName = "Product 2",
                Description = "This is product 2",
                DuluxId = new Guid()
            }    
        };

        dbContext.PaintProducts.AddRange(product);
        dbContext.SaveChanges();
    }
}
