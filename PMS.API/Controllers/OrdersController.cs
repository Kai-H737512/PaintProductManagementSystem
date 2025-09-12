using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PMS.API.DTOs;
using PMS.Models;

namespace PMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private PMSDbContext _dbContext;
        public OrdersController(PMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("PaginatedOrders")]
        public IActionResult GetPaginatedOrders([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            List<Order> filteredOrders = _dbContext.Orders.OrderBy(o => o.OrderId).Skip((pageNumber - 1) * pageSize)
            .Take(pageSize).ToList();
            return Ok(filteredOrders);
        }

        [HttpGet("{orderId}")]
        public IActionResult GetOrderById(int orderId)
        {
            var order = _dbContext.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpGet("{orderId}/get-order-with-products")]
        public IActionResult GetOrderWithProducts(int orderId)
        {
            var order = _dbContext.Orders.Include(o=>o.PaintProducts).FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost]
        public IActionResult CreateEmptyOrder()
        {
            Order order = new Order();
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
            return CreatedAtAction(nameof(GetOrderById), new { orderId = order.OrderId }, order);
        }

        [HttpPut("{orderId}/attach-products-to-order")]
        public IActionResult AttachProductsToOrder(int orderId, [FromBody] AttachPaintProductsToOrderRequest request)
        {
            request.OrderId = orderId;

            var order = _dbContext.Orders.Find(orderId);

            if (order == null)
            {
                return NotFound($"Order {orderId} is not found");
            }

            foreach (var productId in request.PaintProductIds)
            {
                var product = _dbContext.PaintProducts.Find(productId);

                if (product == null)
                {
                    //business logic
                    continue;
                }

                order.PaintProducts.Add(product);
            }
            //try to reduce the call directly to database
            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}
