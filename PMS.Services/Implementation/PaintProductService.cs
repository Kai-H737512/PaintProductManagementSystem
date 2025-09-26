using System;
using PMS.Models;
using PMS.Respositories;
using PMS.Respositories.Interfaces;
using PMS.Services.Interfaces;

namespace PMS.Services;

public class PaintProductService: IPaintProductService
{
    private IPaintProductRepository _paintProductRepository;

    public PaintProductService(IPaintProductRepository paintProductRepository)
    {
        _paintProductRepository = paintProductRepository;
    }

    public PaintProduct CreatePaintProduct(PaintProduct paintProduct)
    {
        throw new NotImplementedException();
    }
}
