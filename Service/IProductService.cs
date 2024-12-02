using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApi.Dto;
using EcommerceApi.Models;

namespace EcommerceApi.Service
{
    public interface IProductService
    {
         Task<ProductDto> SaveProductAsync(ProductDto productDto);
         Task<IEnumerable<ProductDto>> GetAllProductsAsync();
         Task<ProductDto> GetProductById(int Id);

        Task<bool> DeleteProduct(int Id);

        Task<ProductDto> UpdateProduct(int id,ProductDto productDto);
         

    }
}