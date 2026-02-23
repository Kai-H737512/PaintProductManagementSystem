using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMS.Models;

public class OrderPaintProduct
{
    [Key]
    public int Id { get; set; }
    
    public int OrderId { get; set; }
    public int PaintProductId { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public Order Order { get; set; }
    public PaintProduct PaintProduct { get; set; }
    
    public int Quantity { get; set; }
}