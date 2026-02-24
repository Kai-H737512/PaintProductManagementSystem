using System;
using PMS.Repositories;

namespace PMS.Services;

public class PaintProductService
{
    private PaintProductRepository _paintProductReposoitory;

    public PaintProductService(PaintProductRepository paintProductRepository)
    {
        _paintProductReposoitory = paintProductRepository;
    }

}
