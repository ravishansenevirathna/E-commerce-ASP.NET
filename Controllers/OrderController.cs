using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApi.Dto;
using EcommerceApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.Controllers
{
    [Route("api/order")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }


        [HttpPost("save")]
        public async Task<ActionResult<OrderDto>> SaveOrder(OrderDto orderDto){

            if (orderDto == null)
            {
                return BadRequest("Order cannot be null.");
            }

            try
            {
                var savedOrder = await _orderService.SaveOrderAsync(orderDto);
                return CreatedAtAction(nameof(SaveOrder), new { id = savedOrder.Id }, savedOrder);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }



    }
}