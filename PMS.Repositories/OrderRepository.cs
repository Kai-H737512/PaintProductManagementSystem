using System;
using PMS.DataAccess;
using PMS.Models;

namespace PMS.Repositories;

public class OrderRepository
{
    private PMSDbContext _dbContext;

    public OrderRepository(PMSDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Order> GetPaginatedOrders(int pageNumber, int pageSize)
    {
        var orders = _dbContext.Orders.OrderBy(o => o.OrderId).Skip((pageNumber - 1) * pageSize)
            .Take(pageSize).ToList();
        return orders;
    }

    public Order? GetOrderById(int orderId)
    {
        var order = _dbContext.Orders.FirstOrDefault(o => o.OrderId == orderId);
        return order;
    }
}
