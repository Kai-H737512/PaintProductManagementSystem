using System;
using PMS.DataAccess;
using PMS.Models;
using PMS.Respositories.Interfaces;

namespace PMS.Respositories;

public class OrderRepository: IOrderRepository
{
    private PMSDbContext _dbContext;

    public OrderRepository(PMSDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Order? GetOrderById(int orderId)
    {
        return _dbContext.Orders.Find(orderId);
    }

    public List<Order> GetPaginatedOrders(int pageNumber, int pageSize)
    {
        var filteredOrders = _dbContext.Orders.OrderBy(o => o.OrderId).Skip((pageNumber - 1) * pageSize)
             .Take(pageSize).ToList();
        return filteredOrders;
    }
}
