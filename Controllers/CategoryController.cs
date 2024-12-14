using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using EcommerceApi.Dto;
using EcommerceApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.Controllers
{
    [Route("api/category")]
    [ApiController]
    [Authorize]
    public class CategoryController: ControllerBase
    {

        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("save")]
        public async Task<ActionResult<CategoryWithProductDto>> SaveCategoryWithProduct(CategoryWithProductDto categoryWithProductDto){

            if (categoryWithProductDto == null)
            {
                return BadRequest("categoryWithProduct cannot be null.");
            }

            try
            {
                var savedCategoryWithProductDto = await _categoryService.SaveCategoryWithProductAsync(categoryWithProductDto);
                return CreatedAtAction(nameof(SaveCategoryWithProduct), new { id = savedCategoryWithProductDto.CategoryName }, savedCategoryWithProductDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
        [HttpGet("getAllByCategoryName/{categoryName}")]
        public async Task<ActionResult<CategoryWithProductDto>> GetCategoryWithProduct(string categoryName)
        {
            try
            {
                var data = await _categoryService.GetCategoryWithProductAsync(categoryName);
                
                if (data == null)
                {
                    return NotFound(new { message = $"Category with name '{categoryName}' not found." });
                }
                
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while processing your request.", details = ex.Message });
            }
        }




        
    }
}
