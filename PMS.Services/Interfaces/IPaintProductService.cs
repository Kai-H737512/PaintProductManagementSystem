using System;
using PMS.Models;

namespace PMS.Services.Interfaces;

public interface IPaintProductService
{
    PaintProduct CreatePaintProductAsync(PaintProduct paintProduct);
}
