using System;
using Microsoft.EntityFrameworkCore;
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

    public async Task<Order?> GetOrderByIdAsync(int orderId)
    {
        var order = await _dbContext.Orders.FindAsync (orderId);
        return order;
    }

    public async Task<List<Order>> GetPaginatedOrdersAsync(int pageNumber, int pageSize)
    {
        var filteredOrders = await _dbContext.Orders.OrderBy(o => o.OrderId).Skip((pageNumber - 1) * pageSize)
             .Take(pageSize).ToListAsync();
        return filteredOrders;
    }
}
