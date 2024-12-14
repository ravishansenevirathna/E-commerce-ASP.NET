using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApi.Dto;
using EcommerceApi.Models;

namespace EcommerceApi.Service.Impl
{
    public class OrderServiceImpl : IOrderService
    {
        private readonly ProductContext productContext;

        public OrderServiceImpl(ProductContext productContext)
        {
            this.productContext = productContext;
        }

        public async Task<OrderDto> SaveOrderAsync(OrderDto orderDto)
        {
            Order order = new Order
            {
                UserId = orderDto.UserId,
                Amount = orderDto.Amount,
                Status = orderDto.Status,
                Address = orderDto.Address,
                OrderDate = DateTime.Now,
                OrderProducts = new List<OrderProduct>()
            };

            await productContext.Orders.AddAsync(order);

            foreach (var productDto in orderDto.Products)
            {
                var orderProduct = new OrderProduct
                {
                    OrderId = order.Id,  
                    ProductId = productDto.Id,
                    Quantity = productDto.Qty,
                };

                
                order.OrderProducts.Add(orderProduct);
            }

           
            await productContext.SaveChangesAsync();

            OrderDto savedOrderDto = new OrderDto
            {
                UserId = order.UserId,
                Amount = order.Amount,
                Status = order.Status,
                Address = order.Address,
                OrderDate = order.OrderDate,
                Products = order.OrderProducts.Select(op => new ProductDto
                {
                    Id = op.ProductId,  
                    Name = op.Product.Name, 
                    Price = op.Product.Price,
                    Qty = op.Quantity
                }).ToList()
            };

            return savedOrderDto;
        }


   


    }
}