using System;
using PMS.Models;

namespace PMS.Respositories.Interfaces;

public interface IPaintProductRepository
{
    void CreatePaintProduct(PaintProduct paintProduct);
}
