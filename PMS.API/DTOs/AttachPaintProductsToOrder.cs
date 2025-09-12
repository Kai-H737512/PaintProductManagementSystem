using System;

namespace PMS.API.DTOs;

public class AttachPaintProductsToOrderRequest //DTO for attach products to order endpoint
{
    public int OrderId { get; set; }
    public List<int> PaintProductIds { get; set; } = new List<int>();
}
