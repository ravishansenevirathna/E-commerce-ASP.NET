using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApi.Dto;
using EcommerceApi.Models;
using EcommerceApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Controllers
{
    [Route("api/products-api")]
    [ApiController]
    [Authorize] // Ensures all endpoints require a valid token
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

         public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpPost("save")]
        public async Task<ActionResult<ProductDto>> SaveProduct(ProductDto productDto){

            if (productDto == null)
            {
                return BadRequest("Product cannot be null.");
            }

            try
            {
                var savedProduct = await _productService.SaveProductAsync(productDto);
                return CreatedAtAction(nameof(SaveProduct), new { id = savedProduct.Id }, savedProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

    
        [HttpGet("get-all")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts(){
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }


        [HttpGet("get-product-by/{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id){

            var product = await _productService.GetProductById(id);

            if(product == null){
                return NotFound();
            }

            return product;
        }

        [HttpDelete("delete-product-by/{id}")]
        public async Task<ActionResult> DeleteProduct(int id){

            var success = await _productService.DeleteProduct(id);

        if (!success)
        {
            return NotFound(new { message = "Product not found" });
        }

        return Ok(new { message = "Product deleted successfully" });
        }


        [HttpPut("update-product-by/{id}")]
        public async Task<ActionResult<ProductDto>> UpdateProduct(int id,ProductDto productDto){

            if (productDto == null)
            {
                return BadRequest("Product cannot be null.");
            }

            try
            {
                var updatedProduct = await _productService.UpdateProduct(id,productDto);
                return updatedProduct;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

    }
}