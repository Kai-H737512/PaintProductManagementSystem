using System;
using PMS.Respositories;
using PMS.Services.Interfaces;

namespace PMS.Services;

public class PaintProductService: IPaintProductService
{
    private PaintProductRepository _paintProductRepository;

    public PaintProductService(PaintProductRepository paintProductRepository)
    {
        _paintProductRepository = paintProductRepository;
    }
}
