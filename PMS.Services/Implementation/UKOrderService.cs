using System;
using PMS.Models;
using PMS.Respositories;
using PMS.Services.Interfaces;

namespace PMS.Services;

public class UKOrderService: IOrderService
{
    private OrderRepository _orderRepository;
    private PaintProductRepository _paintProductRepository;

    public UKOrderService(OrderRepository orderRepository, PaintProductRepository paintProductRepository)
    {
        _orderRepository = orderRepository;
        _paintProductRepository = paintProductRepository;
    }

    public Order GetOrderById(int orderId)
    {
        var order = _orderRepository.GetOrderById(orderId);

        if (order == null)
        {
            throw new Exception($"Order: {orderId} is not found");
        }
        
        return order;
    }

    public List<Order> GetPaginatedOrders(int pageNumber, int pageSize)
    {
        if (pageNumber <= 0 || pageSize <= 0)
        {
            throw new ArgumentOutOfRangeException("Page numberor or Page size is invalid");
        }
        var orders = _orderRepository.GetPaginatedOrders(pageNumber, pageSize);

        if (orders == null || orders.Count == 0)
        {
            throw new Exception($"Can not fetch orders for condition: pageNumber: {pageNumber}, pageSize:{pageSize}");
        }

        return orders;
    }
}
