using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceApi.Config.Auth
{
    public class OtpGenerator
    {
         public static string GenerateOtp()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString(); // Generates a 6-digit OTP
        }
    }
}