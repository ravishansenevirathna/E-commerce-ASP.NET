using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApi.Dto;
using EcommerceApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Service.Impl
{
    public class ProductServiceImpl : IProductService
    {
        private readonly ProductContext productContext;

        public ProductServiceImpl(ProductContext productContext)
        {
            this.productContext = productContext;
        }


        public async Task<ProductDto> SaveProductAsync(ProductDto productDto)
        {
            Product product = new Product();
            product.Name = productDto.Name;
            product.Price = productDto.Price;
            product.Qty = productDto.Qty;
            product.Category = productDto.Category;
            product.CreatedAt = DateTime.Now;

            productContext.Products.Add(product);
            await productContext.SaveChangesAsync();

            ProductDto savedProductDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Qty = product.Qty,
                Category = product.Category
            };

            return savedProductDto;
        }

       public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            List<Product> products = await productContext.Products.ToListAsync();

            
            List<ProductDto> productDtos = products.Select(product => new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Qty = product.Qty,
                Category = product.Category,
                CreatedAt = product.CreatedAt
            }).ToList();

            return productDtos;
        }

        public async Task<ProductDto> GetProductById(int productId)
        {
            var product = await productContext.Products.FindAsync(productId);
            return product == null ? null : new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Qty = product.Qty,
                    Category = product.Category,
                    CreatedAt = product.CreatedAt
                };
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            var product = await productContext.Products.FindAsync(productId);

            if (product == null)
            {
                return false;
            }

            productContext.Products.Remove(product);
            await productContext.SaveChangesAsync();

            return true;
        }

       public async Task<ProductDto> UpdateProduct(int productId, ProductDto productDto)
        {
            var existingProduct = await productContext.Products.FindAsync(productId);

            if (existingProduct == null)
            {
                return null;
            }

            existingProduct.Name = productDto.Name;
            existingProduct.Price = productDto.Price;
            existingProduct.Qty = productDto.Qty;
            existingProduct.Category = productDto.Category;
            existingProduct.UpdatedAt = DateTime.Now;

            productContext.Products.Update(existingProduct);
            await productContext.SaveChangesAsync();

            return new ProductDto
            {
                Id = existingProduct.Id,
                Name = existingProduct.Name,
                Price = existingProduct.Price,
                Qty = existingProduct.Qty,
                Category = existingProduct.Category,
                CreatedAt = existingProduct.CreatedAt,
                UpdatedAt = existingProduct.UpdatedAt
            };
        }

    }
}