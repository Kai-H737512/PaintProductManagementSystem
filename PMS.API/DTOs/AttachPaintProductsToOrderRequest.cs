using System;
using PMS.Models;

namespace PMS.API.DTOs;

public class AttachPaintProductsToOrderRequest
{
  public int OrderId { get; set; }
  public List<int> PaintProductsId { get; set; } = new List<int>();

}