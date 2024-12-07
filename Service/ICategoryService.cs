using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApi.Dto;

namespace EcommerceApi.Service
{
    public interface ICategoryService
    {
        Task<CategoryWithProductDto> SaveCategoryWithProductAsync(CategoryWithProductDto categoryWithProductDto);
        Task<CategoryWithProductDto> GetCategoryWithProductAsync(string categoryName);
        
    }
}