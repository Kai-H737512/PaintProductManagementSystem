using System;

namespace PMS.Models;

public class Order
{
    public int OrderId { get; set; }

    public List<PaintProduct> PaintProducts { get; set; }

    public Order(int orderId)
    {
        OrderId = orderId;
        PaintProducts = new List<PaintProduct>();
    }
}
