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


        public async Task<ProductDto> SaveProductAsync(ProductDto productDto,IFormFile image)
        {
            // Save the image to the uploads folder
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            // Set the image path in the DTO
            productDto.ImagePath = $"uploads/{fileName}";



            Product product = new Product();
            product.Name = productDto.Name;
            product.Price = productDto.Price;
            product.Qty = productDto.Qty;
            product.CreatedAt = DateTime.Now;
            product.CategoryId = productDto.CategoryId;
            product.ImagePath = productDto.ImagePath;

            productContext.Products.Add(product);
            await productContext.SaveChangesAsync();

            ProductDto savedProductDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Qty = product.Qty,
                ImagePath = product.ImagePath
        
            };

            return savedProductDto;
        }

       public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            List<Product> products = await productContext.Products.ToListAsync();

            string baseUrl = "http://localhost:5018/"; 
            
            List<ProductDto> productDtos = products.Select(product => new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Qty = product.Qty,
            
                CreatedAt = product.CreatedAt,
                ImagePath = !string.IsNullOrEmpty(product.ImagePath) ? baseUrl + product.ImagePath : null

            }).ToList();

            return productDtos;
        }

        public async Task<ProductDto> GetProductById(int productId)
        {

            string baseUrl = "http://localhost:5018/"; 

            var product = await productContext.Products.FindAsync(productId);
            return product == null ? null : new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Qty = product.Qty,
                    CreatedAt = product.CreatedAt,
                    ImagePath = !string.IsNullOrEmpty(product.ImagePath) ? baseUrl + product.ImagePath : null
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
            existingProduct.UpdatedAt = DateTime.Now;

            productContext.Products.Update(existingProduct);
            await productContext.SaveChangesAsync();

            return new ProductDto
            {
                Id = existingProduct.Id,
                Name = existingProduct.Name,
                Price = existingProduct.Price,
                Qty = existingProduct.Qty,
             
                CreatedAt = existingProduct.CreatedAt,
                UpdatedAt = existingProduct.UpdatedAt
            };
        }

    }
}