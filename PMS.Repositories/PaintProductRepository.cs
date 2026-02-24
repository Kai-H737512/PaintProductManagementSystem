using System;
using PMS.DataAccess;
using PMS.Models;

namespace PMS.Repositories;

public class PaintProductRepository
{
    private PMSDbContext _dbContext;
    public PaintProductRepository(PMSDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Order> GetPaginatedOrders(int pageNumber, int pageSize)
    {
        var filteredOrders = _dbContext.Orders.OrderBy(o => o.OrderId).Skip((pageNumber - 1) * pageSize)
            .Take(pageSize).ToList();
        return filteredOrders;
    }
}
