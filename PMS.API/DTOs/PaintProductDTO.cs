using System;

namespace PMS.API.DTOs;

public class PaintProductDTO
{
    public int Id { get; set; }
    public string PaintProductName { get; set; }

    public PaintProductDTO(int id, string paintProductName)
    {
        PaintProductName = paintProductName;
        Id = Id;
    }
}
