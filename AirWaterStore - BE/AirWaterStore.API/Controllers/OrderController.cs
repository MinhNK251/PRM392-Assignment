using AirWaterStore.API.DTOs;
using AirWaterStore.Business.Interfaces;
using AirWaterStore.Business.Services;
using AirWaterStore.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirWaterStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/order?pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var orders = await _orderService.GetAllAsync(pageNumber, pageSize);
            var total = await _orderService.GetTotalCountAsync();

            return Ok(new
            {
                totalCount = total,
                data = orders
            });
        }

        // GET: api/order/user/5?pageNumber=1&pageSize=10
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetAllByUserIdAsync(int userId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var orders = await _orderService.GetAllByUserIdAsync(userId, pageNumber, pageSize);
            var total = await _orderService.GetTotalCountByUserIdAsync(userId);

            return Ok(new
            {
                totalCount = total,
                data = orders
            });
        }

        // GET: api/order/5
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetByIdAsync(int orderId)
        {
            var order = await _orderService.GetByIdAsync(orderId);
            if (order == null)
                return NotFound();

            return Ok(order);
        }

        // POST: api/order
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateOrderDto dto)
        {
            var order = new Order
            {
                UserId = dto.UserId,
                OrderDate = DateTime.Now,
                TotalPrice = dto.TotalPrice,
                Status = dto.Status,
            };
            await _orderService.AddAsync(order);
            return CreatedAtAction(nameof(GetByIdAsync), new { orderId = order.OrderId }, order);
        }

        // PUT: api/order/5
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateOrderDto dto)
        {
            var existing = await _orderService.GetByIdAsync(dto.OrderId);
            if (existing == null)
                return NotFound();
            existing.Status = dto.Status;
            await _orderService.UpdateAsync(existing);
            return NoContent();
        }
    }
}
