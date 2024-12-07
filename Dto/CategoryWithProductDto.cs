using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceApi.Dto
{
    public class CategoryWithProductDto
    {
        public string CategoryName{get; set;}

        public string CategoryCode{get; set;}

        public List<ProductDto> Products{get; set;}
    }
}