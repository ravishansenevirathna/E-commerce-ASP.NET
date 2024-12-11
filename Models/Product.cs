using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceApi.Models
{
    public class Product
    {
        public int Id{get; set;}
        public string Name{get; set;}
        public decimal Price{get; set;}
        public int Qty{get; set;}

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // forign key
        public int? CategoryId{get; set;}

        // relationship define -- this column is used only for relationship so there is no any column in the Product table
        public Category Category{get; set; }

        public string ImagePath { get; set; }



    }
}