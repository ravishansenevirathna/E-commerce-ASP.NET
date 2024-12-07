using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApi.Dto;
using EcommerceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Service.Impl
{
    public class CategoryServiceImpl:ICategoryService
    {
        private readonly ProductContext productContext;

        public CategoryServiceImpl(ProductContext productContext)
        {
            this.productContext = productContext;
        }
        
        
        public async Task<CategoryWithProductDto> SaveCategoryWithProductAsync(CategoryWithProductDto categoryWithProductDto)
        {
            Category category = new Category
            {
                CategoryName = categoryWithProductDto.CategoryName,
                CategoryCode = categoryWithProductDto.CategoryCode,
                Products = categoryWithProductDto.Products.Select(p => new Product
                {
                    Name = p.Name,
                    Price = p.Price,
                    Qty = p.Qty
                }).ToList()
            };
            
            await productContext.AddAsync(category);
            
            await productContext.SaveChangesAsync();
            
            CategoryWithProductDto savedCategoryDto = new CategoryWithProductDto
            {
                CategoryName = category.CategoryName,
                CategoryCode = category.CategoryCode,
                Products = category.Products.Select(p => new ProductDto
                {
                    Name = p.Name,
                    Price = p.Price,
                    Qty = p.Qty
                }).ToList()
            };

            return savedCategoryDto;
        }

        public async Task<CategoryWithProductDto> GetCategoryWithProductAsync(string categoryName)
        {
            var category = await productContext.Categories
                .Include(c => c.Products) 
                .FirstOrDefaultAsync(c => c.CategoryName == categoryName);
            
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with name '{categoryName}' not found.");
            }
            
            var categoryWithProductDto = new CategoryWithProductDto
            {
                CategoryName = category.CategoryName,
                CategoryCode = category.CategoryCode,
                Products = category.Products.Select(p => new ProductDto
                {
                    Name = p.Name,
                    Price = p.Price,
                    Qty = p.Qty
                }).ToList()
            };

            return categoryWithProductDto;
        }
        
    }
}