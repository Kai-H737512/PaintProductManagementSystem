using System;
using PMS.Models;

namespace PMS.Respositories.Interfaces;

public interface IOrderRepository
{
    Task<Order?> GetOrderByIdAsync(int orderId);

    Task<List<Order>> GetPaginatedOrdersAsync(int pageNumber, int pageSize);
}
