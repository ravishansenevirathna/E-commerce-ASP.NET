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
    [Route("api/payment")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
   {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("process")]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentDto paymentDto)
        {
            var result = await _paymentService.ProcessPaymentAsync(paymentDto);
            return Ok(result);
        }

        [HttpGet("get-payment-details/{id}")]
        public async Task<IActionResult> GetPaymentDetails(int id)
        {
            var result = await _paymentService.GetPaymentDetailsAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPatch("update-payment-details/{id}")]
        public async Task<IActionResult> UpdatePaymentStatus(int id, [FromQuery] string status)
        {
            var result = await _paymentService.UpdatePaymentStatusAsync(id, status);
            return Ok(result);
        }
    }
}