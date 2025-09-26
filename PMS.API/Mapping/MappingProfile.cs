using System;
using AutoMapper;
using PMS.API.DTOs;
using PMS.Models;

namespace PMS.API.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreatePaintProductRequest, PaintProduct>();
        CreateMap<PaintProduct, PaintProductDto>();
    }
}
