using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Controllers
{
    [Route("api/products-api")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ProductContext productContext;

        public ProductController(ProductContext productContext){
            this.productContext = productContext;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> SaveProduct(Product product){

            productContext.Add(product);
            await productContext.SaveChangesAsync();

            return product;
        }

    
        [HttpGet("custom-get-products")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts(){
            return await productContext.Products.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductsById(int id){
            var product = await productContext.Products.FindAsync(id);

            if(product == null){
                return NotFound();
            }

            return product;
        }


    }
}