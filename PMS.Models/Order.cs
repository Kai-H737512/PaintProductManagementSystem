using System;

namespace PMS.Models;

public class Order
{
    public int OrderId { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!; // added User reference
    public DateTime CreatedAt { get; set; }

    public List<OrderPaintProduct> OrderPaintProducts { get; set; } = new List<OrderPaintProduct>();
    

    public Order()
    {
        OrderPaintProducts = new List<OrderPaintProduct>();
    }
}
