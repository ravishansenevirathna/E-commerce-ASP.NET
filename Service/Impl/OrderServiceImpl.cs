using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApi.Dto;
using EcommerceApi.Models;
using QRCoder;

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

            string qrContent = $"OrderId: {order.Id}, Amount: {order.Amount}, UserId: {order.UserId}";
            string qrCodeBase64 = GenerateQRCode(qrContent);

            OrderDto savedOrderDto = new OrderDto
            {
                UserId = order.UserId,
                Amount = order.Amount,
                Status = order.Status,
                Address = order.Address,
                OrderDate = order.OrderDate,
                QRCode = qrCodeBase64,
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

        

            private string GenerateQRCode(string content)
        {
            using (var qrGenerator = new QRCoder.QRCodeGenerator())
            {
                // Generate the QR code data
                var qrCodeData = qrGenerator.CreateQrCode(content, QRCoder.QRCodeGenerator.ECCLevel.Q);

                // Use Base64QRCode to directly get the Base64-encoded string
                var base64QRCode = new QRCoder.Base64QRCode(qrCodeData);
                return base64QRCode.GetGraphic(20);
            }
        }
   


    }
}