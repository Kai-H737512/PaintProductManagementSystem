using System;

namespace PMS.API.DTOs;

public class PaintProductDto
{
    public int Id { get; set; }
    public string PaintProductName { get; set; }

    public PaintProductDto(int id, string paintProductName)
    {
        PaintProductName = paintProductName;
        Id = id;
    }
}
