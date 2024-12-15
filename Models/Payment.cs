using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceApi.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } // e.g., Credit Card, PayPal
        public string PaymentStatus { get; set; } // e.g., Success, Failed, Pending
        public DateTime PaymentDate { get; set; }

        // Relationship
        public Order Order { get; set; }
    }
}