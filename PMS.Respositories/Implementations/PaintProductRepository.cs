using System;
using PMS.DataAccess;
using PMS.Models;
using PMS.Respositories.Interfaces;

namespace PMS.Respositories;

public class PaintProductRepository: IPaintProductRepository
{
    private PMSDbContext _dbContext;

    public PaintProductRepository(PMSDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void CreatePaintProduct(PaintProduct paintProduct)
    {
        _dbContext.PaintProducts.Add(paintProduct);
        _dbContext.SaveChanges();
    }
}
