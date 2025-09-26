using System;
using System.ComponentModel.DataAnnotations;

namespace PMS.API.DTOs;

public class CreatePaintProductRequest
{
    public string PaintProductName { get; set; }
    
    public string Description { get; set; }
    public Guid DuluxId { get; set; }

    public CreatePaintProductRequest(string paintProductName, string description, Guid duluxId)
    {
        PaintProductName = paintProductName;
        Description = description;
        DuluxId = duluxId;
    }
}
