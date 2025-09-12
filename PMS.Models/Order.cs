using System;

namespace PMS.Models;

public class Order
{
    //Convention based 约定驱动
    //PK
    public int OrderId { get; set; } //classname+Id

    public List<PaintProduct> PaintProducts { get; set; }

    public Order()
    {
        PaintProducts = new List<PaintProduct>();
    }
}
