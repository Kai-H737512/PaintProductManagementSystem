using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PMS.API.DTOs;
using PMS.Models;
using PMS.Services;
using PMS.Services.Interfaces;

namespace PMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaintProductsController : ControllerBase
    {
        private IPaintProductService _paintProductService;
        private IMapper _mapper;
        public PaintProductsController(IPaintProductService paintProductService, IMapper mapper)
        {
            _paintProductService = paintProductService;
            _mapper = mapper;
        }

        // [HttpGet]
        // public IActionResult GetPaintProducts()
        // {
        //     List<PaintProduct> paintProducts = _dbContext.PaintProducts.ToList();
        //     return Ok(paintProducts);
        // }

        // [HttpGet("first")]
        // public IActionResult GetFirstPaintProduct()
        // {
        //     return Ok(_dbContext.PaintProducts.FirstOrDefault());
        // }

        // //新增一个endpoint, 返回 paintProducts中 匹配传入的id === productId 的product

        [HttpGet("{productId}")]
        public IActionResult GetProductById(int productId)
        {
            // var paintProduct = _paintProductService.
            return Ok();
        }

        //  [HttpGet("{productId}/get-product-with-orders")]
        // public IActionResult GetProductWithOrderById(int productId)
        // {
        //     var paintProduct = _dbContext.PaintProducts.Include(p=>p.Orders).FirstOrDefault(p => p.Id == productId);
        //     return Ok(paintProduct);
        // }

        // [HttpDelete("{productId}")]
        // public IActionResult DeleteProductById(int productId)
        // {
        //     var paintProduct = _dbContext.PaintProducts.FirstOrDefault(p => p.Id == productId);
        //     return Ok(paintProduct);
        // }

        // [HttpPut("{productId}")]
        // public IActionResult UpdateProductById(int productId)
        // {
        //     var paintProduct = _dbContext.PaintProducts.FirstOrDefault(p => p.Id == productId);
        //     return Ok(paintProduct);
        // }

        // //创建一个新的接口来创建新的product
        // //REST design,  URI 一致性，尽量去利用URI 来描述这个接口的行为， 而不是用routing名称
        [HttpPost]
        public IActionResult CreatePaintProduct([FromBody] CreatePaintProductRequest request) //Model binding, 模型绑定， parameter, map from incoming http request body
        {
            var paintProduct = _mapper.Map<PaintProduct>(request);
            var createdPaintProduct = _paintProductService.CreatePaintProduct(paintProduct);
            
            var dto = _mapper.Map<PaintProductDTO>(paintProduct);
            return CreatedAtAction(nameof(GetProductById), new { productId = paintProduct.Id }, dto);
        }

        // //创建一个新的endpoint, Filter products, filter id > minId 并且 小于 maxId 的product
        // //?minId=XX&maxId=YY
        // [HttpGet("Filter")]
        // public IActionResult FilterProductById([FromQuery] int minId, [FromQuery] int maxId)
        // {
        //     //LINQ
        //     List<PaintProduct> filteredProducts = _dbContext.PaintProducts.Where(p => p.Id > minId && p.Id < maxId).ToList();
        //     return Ok(filteredProducts);
        // }
    }
}
