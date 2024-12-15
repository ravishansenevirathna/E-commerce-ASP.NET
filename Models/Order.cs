using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceApi.Models
{
    public class Order
    {
        public int Id{get; set;}
        public int UserId{get; set;}

        public decimal Amount{get; set;}

        public string Status{get; set;}

        public string Address{get; set;}

        public DateTime OrderDate{get; set;}

        public ICollection<OrderProduct> OrderProducts { get; set; }

         public ICollection<Payment> Payments { get; set; }


    }
}