using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApi.Dto;

namespace EcommerceApi.Service
{
    public interface IPaymentService
    {
        Task<PaymentDto> ProcessPaymentAsync(PaymentDto paymentDto);
        Task<PaymentDto> GetPaymentDetailsAsync(int paymentId);
        Task<PaymentDto> UpdatePaymentStatusAsync(int paymentId, string status);
    }
}