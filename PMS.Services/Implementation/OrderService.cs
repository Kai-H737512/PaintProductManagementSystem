using System;
using PMS.Models;
using PMS.Repositories;
using PMS.Services.Interfaces;

namespace PMS.Services;

public class OrderService : IOrderServices
{
    private OrderRepository _orderRepository;
    private PaintProductRepository _paintProductRepository;

    public OrderService(OrderRepository orderRepository, PaintProductRepository paintProductRepository)
    {
        _orderRepository = orderRepository;
        _paintProductRepository = paintProductRepository;
    }

    public List<Order> GetPaginatedOrders(int pageNumber, int pageSize)
    {
        var orders = _orderRepository.GetPaginatedOrders(pageNumber, pageSize);
        if ( orders  == null || orders.Count == 0)
        {
            throw new Exception($"Can not fetch orders for pageNumber: {pageNumber}, pageSize: {pageSize} ");
        }

        return orders;
    }

    public Order GetOrderById(int orderId)
    {
        var order = _orderRepository.GetOrderById(orderId);
        if (order == null)
            {
                throw new Exception($"Can not find order with orderId: {orderId}");
            }

        return order;
    }
}
