using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Models;

namespace PMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private List<Order> Orders;

        public OrdersController()
        {
            Orders = new List<Order>();
        }

        [HttpGet("PaginatedOrders")]
        public IActionResult GetPaginatedOrders([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            List<Order> filteredOrders = Orders.OrderBy(o => o.OrderId).Skip((pageNumber - 1) * pageSize)
            .Take(pageSize).ToList();
            return Ok(filteredOrders);
        }
    }
}
