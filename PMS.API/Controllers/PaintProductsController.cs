using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PMS.Models;

namespace PMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaintProductsController : ControllerBase
    {
        private PMSDbContext _dbContext;
        public PaintProductsController(PMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetPaintProducts()
        {
            List<PaintProduct> paintProducts = _dbContext.PaintProducts.ToList();
            return Ok(paintProducts);
        }

        [HttpGet("first")]
        public IActionResult GetFirstPaintProduct()
        {
            return Ok(_dbContext.PaintProducts.FirstOrDefault());
        }

        //新增一个endpoint, 返回 paintProducts中 匹配传入的id === productId 的product

        [HttpGet("{productId}")]
        public IActionResult GetProductById(int productId)
        {
            var paintProduct = _dbContext.PaintProducts.FirstOrDefault(p => p.Id == productId);
            return Ok(paintProduct);
        }

        [HttpDelete("{productId}")]
        public IActionResult DeleteProductById(int productId)
        {
            var paintProduct = _dbContext.PaintProducts.FirstOrDefault(p => p.Id == productId);
            return Ok(paintProduct);
        }

        [HttpPut("{productId}")]
        public IActionResult UpdateProductById(int productId)
        {
            var paintProduct = _dbContext.PaintProducts.FirstOrDefault(p => p.Id == productId);
            return Ok(paintProduct);
        }

        //创建一个新的接口来创建新的product
        //REST design,  URI 一致性，尽量去利用URI 来描述这个接口的行为， 而不是用routing名称
        [HttpPost]
        public IActionResult CreatePaintProduct([FromBody] PaintProduct paintProduct) //Model binding, 模型绑定， parameter, map from incoming http request body
        {
            Console.WriteLine(">>> HIT CreatePaintProduct <<<");
            _dbContext.PaintProducts.Add(paintProduct);
            _dbContext.SaveChanges();
            // return StatusCode((int)HttpStatusCode.Created);
            // return StatusCode(201);
            // return Created($"/api/PaintProducts/{paintProduct.Id}", paintProduct);
            return CreatedAtAction(nameof(GetProductById), new { productId = paintProduct.Id }, paintProduct);
        }

        //创建一个新的endpoint, Filter products, filter id > minId 并且 小于 maxId 的product
        //?minId=XX&maxId=YY
        [HttpGet("Filter")]
        public IActionResult FilterProductById([FromQuery] int minId, [FromQuery] int maxId)
        {
            //LINQ
            List<PaintProduct> filteredProducts = _dbContext. PaintProducts.Where(p => p.Id > minId && p.Id < maxId).ToList();
            return Ok(filteredProducts);
        }

        [HttpGet("{productId}/get-paintproduct-with-order")]
        public IActionResult GetProductWithOrder(int productId)        
        {
            var paintProduct = _dbContext.PaintProducts.Include(p=>p.Orders).FirstOrDefault(p => p.Id == productId);
            if (paintProduct == null)
            {
                return NotFound($"Product {productId} not found");
            }
            return Ok(paintProduct);
        }
    }
}
