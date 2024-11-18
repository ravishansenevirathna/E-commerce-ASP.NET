using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
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
    }
}