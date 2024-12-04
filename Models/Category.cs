using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceApi.Models
{
    public class Category
    {
        public int Id{get; set;}
        public string CategoryName{get; set;}

        public string CategoryCode{get; set;}

        public ICollection<Product> Products{get; set;}
    }
}