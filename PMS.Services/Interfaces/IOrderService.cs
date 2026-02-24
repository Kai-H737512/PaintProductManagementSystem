using System;
using PMS.Models;

namespace PMS.Services.Interfaces;

public interface IOrderServices
{
    List<Order> GetPaginatedOrders(int pageNumber, int pageSize);
    Order GetOrderById(int orderId);
}
