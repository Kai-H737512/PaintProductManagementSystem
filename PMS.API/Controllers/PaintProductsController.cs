using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Models;

namespace PMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaintProductsController : ControllerBase
    {
        private readonly List<PaintProduct> PaintProducts;

        public PaintProductsController()
        {
            PaintProducts = new List<PaintProduct>();
        }

        [HttpGet]
        public IActionResult GetPaintProducts()
        {
            return Ok(PaintProducts);
        }

        [HttpGet("first")]
        public IActionResult GetFirstPaintProduct()
        {
            return Ok(PaintProducts.FirstOrDefault());
        }

        //新增一个endpoint, 返回 paintProducts中 匹配传入的id === productId 的product

        [HttpGet("{productId}")]
        public IActionResult GetProductById(int productId)
        {
            var paintProduct = PaintProducts.FirstOrDefault(p => p.Id == productId);
            return Ok(paintProduct);
        }

        //创建一个新的接口来创建新的product
        //REST design,  URI 一致性，尽量去利用URI 来描述这个接口的行为， 而不是用routing名称
        [HttpPost]
        public IActionResult CreatePaintProduct([FromBody] PaintProduct paintProduct) //Model binding, 模型绑定， parameter, map from incoming http request body
        {
            PaintProducts.Add(paintProduct);
            // return StatusCode((int)HttpStatusCode.Created);
            // return StatusCode(201);
            // return Created($"/api/PaintProducts/{paintProduct.Id}", paintProduct);
            return CreatedAtAction(nameof(GetProductById),  new { paintProduct.Id}, paintProduct);
        }
    }
}
