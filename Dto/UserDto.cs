using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceApi.Dto
{
    public class UserDto
    {
        public int Id{get; set;}
        public string UserName{get; set;}
        public string Email{get; set;}
        public string Password{get; set;}

        public string? OtpCode{get; set;}

        public DateTime OtpExpiry { get; set; }

        public bool IsOtpVerified{get; set;}

        public string? Token { get; set; }
    }
}