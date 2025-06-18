using AirWaterStore.API.DTOs;
using AirWaterStore.Business.Interfaces;
using AirWaterStore.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirWaterStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;

        public OrderDetailController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        // GET: api/orderdetail/order/5
        [HttpGet("order/{orderId}")]
        public async Task<IActionResult> GetAllByOrderIdAsync(int orderId)
        {
            var details = await _orderDetailService.GetAllByOrderIdAsync(orderId);
            return Ok(details);
        }

        // POST: api/orderdetail
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] List<CreateOrderDetailDto> dtos)
        {
            foreach (var item in dtos)
            {
                var detail = new OrderDetail
                {
                    OrderId = item.OrderId,
                    GameId = item.GameId,
                    Quantity = item.Quantity,
                    Price = item.Price
                };

                await _orderDetailService.AddAsync(detail);
            }

            return Ok(new { Message = "Order details added successfully", dtos.Count });
        }
    }
}
