using System;
using PMS.Models;

namespace PMS.Services.Interfaces;

public interface IOrderService
{
    Task<List<Order>> GetPaginatedOrdersAsync(int pageNumber, int pageSize);
    Task<Order> GetOrderByIdAsync(int orderId);
}
