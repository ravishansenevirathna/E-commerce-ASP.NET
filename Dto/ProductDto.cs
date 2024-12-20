using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApi.Models;

namespace EcommerceApi.Dto
{
    public class ProductDto
    {
        public int Id{get; set;}
        public string Name{get; set;}
        public decimal Price{get; set;}
        public int Qty{get; set;}
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int CategoryId{get; set;}
        public string? ImagePath { get; set; }
    
    }
}