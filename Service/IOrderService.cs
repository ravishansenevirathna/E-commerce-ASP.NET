using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApi.Dto;

namespace EcommerceApi.Service
{
    public interface IOrderService
    {
        
         Task<OrderDto> SaveOrderAsync(OrderDto orderDto);
    }
}