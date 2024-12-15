using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApi.Dto;
using EcommerceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Service.Impl
{
    public class PaymentServiceImpl : IPaymentService
   {
        private readonly ProductContext _productContext;

        public PaymentServiceImpl(ProductContext productContext)
        {
            _productContext = productContext;
        }

        public async Task<PaymentDto> ProcessPaymentAsync(PaymentDto paymentDto)
        {
            var payment = new Payment
            {
                OrderId = paymentDto.OrderId,
                Amount = paymentDto.Amount,
                PaymentMethod = paymentDto.PaymentMethod,
                PaymentStatus = "Pending",
                PaymentDate = DateTime.UtcNow
            };

            await _productContext.Payments.AddAsync(payment);
            await _productContext.SaveChangesAsync();

            return new PaymentDto
            {
                Id = payment.Id,
                OrderId = payment.OrderId,
                Amount = payment.Amount,
                PaymentMethod = payment.PaymentMethod,
                PaymentStatus = payment.PaymentStatus,
                PaymentDate = payment.PaymentDate
            };
        }

        public async Task<PaymentDto> GetPaymentDetailsAsync(int paymentId)
        {
            var payment = await _productContext.Payments
                .Include(p => p.Order) 
                .FirstOrDefaultAsync(p => p.Id == paymentId);

            if (payment == null) return null;

            return new PaymentDto
            {
                Id = payment.Id,
                OrderId = payment.OrderId,
                Amount = payment.Amount,
                PaymentMethod = payment.PaymentMethod,
                PaymentStatus = payment.PaymentStatus,
                PaymentDate = payment.PaymentDate
            };
        }

        public async Task<PaymentDto> UpdatePaymentStatusAsync(int paymentId, string status)
        {
            var payment = await _productContext.Payments.FindAsync(paymentId);

            if (payment == null) throw new InvalidOperationException("Payment not found");

            payment.PaymentStatus = status;
            await _productContext.SaveChangesAsync();

            return new PaymentDto
            {
                Id = payment.Id,
                OrderId = payment.OrderId,
                Amount = payment.Amount,
                PaymentMethod = payment.PaymentMethod,
                PaymentStatus = payment.PaymentStatus,
                PaymentDate = payment.PaymentDate
            };
        }
    }
}