using System;
using PMS.Models;

namespace PMS.Respositories.Interfaces;

public interface IOrderRepository
{
    Order? GetOrderById(int orderId);

    List<Order> GetPaginatedOrders(int pageNumber, int pageSize);
}
