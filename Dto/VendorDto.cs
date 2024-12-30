using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceApi.Dto
{
    public class VendorDto
    {
         public int Id{get; set;}

        public string Venodr_u_id{get; set;}

        public string Owner_Name{get; set;}

        public string Business_Name{get; set;}

        public string Business_Address{get; set;}

        public string Bank_Account{get; set;}
        
        public string Email{get; set;}
        public string Nic{get; set;}
        public string Mobile{get; set;}

        public int Admin_Approved{get; set;}
    }
}