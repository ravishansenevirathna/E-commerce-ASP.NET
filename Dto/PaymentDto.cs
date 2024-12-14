using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceApi.Dto
{
    public class PaymentDto
    {
         public int Id{get; set;}

        public string PaymentMethod{get; set;}

        public double Amount{get; set;}

        public int OrderId{get; set;}

        public DateTime deliveryDate{get; set;}

        public string DeliveryStatus{get; set;}
    }
}