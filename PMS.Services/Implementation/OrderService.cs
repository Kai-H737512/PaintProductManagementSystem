using System;
using PMS.Models;
using PMS.Respositories;
using PMS.Respositories.Interfaces;
using PMS.Services.Interfaces;

namespace PMS.Services;

public class OrderService: IOrderService
{
    private IOrderRepository _orderRepository;
    private IPaintProductRepository _paintProductRepository;

    public OrderService(IOrderRepository orderRepository, IPaintProductRepository paintProductRepository)
    {
        _orderRepository = orderRepository;
        _paintProductRepository = paintProductRepository;
    }

    public async Task<Order> GetOrderByIdAsync(int orderId)
    {
        var order = await _orderRepository.GetOrderByIdAsync(orderId);

        if (order == null)
        {
            throw new KeyNotFoundException($"Order: {orderId} is not found");
        }
        
        return order;
    }

    public async Task<List<Order>> GetPaginatedOrdersAsync(int pageNumber, int pageSize)
    {
        if (pageNumber <= 0 || pageSize <= 0)
        {
            throw new ArgumentOutOfRangeException("Page numberor or Page size is invalid");
        }
        var orders = await _orderRepository.GetPaginatedOrdersAsync(pageNumber, pageSize);

        if (orders == null || orders.Count == 0)
        {
            throw new Exception($"Can not fetch orders for condition: pageNumber: {pageNumber}, pageSize:{pageSize}");
        }

        return orders;
    }
}
