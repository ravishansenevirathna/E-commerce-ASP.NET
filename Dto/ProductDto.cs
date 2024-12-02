using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceApi.Dto
{
    public class ProductDto
    {
        public int Id{get; set;}
        public string Name{get; set;}
        public decimal Price{get; set;}
        public int Qty{get; set;}
        public string Category{get; set;}
    
    }
}