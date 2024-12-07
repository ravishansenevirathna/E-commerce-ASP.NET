using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApi.Config;
using EcommerceApi.Config.Auth;
using EcommerceApi.Dto;
using EcommerceApi.Models;

namespace EcommerceApi.Service.Impl
{
    public class UserServiceImpl : IUserService
    {
        private readonly ProductContext productContext;
        private readonly EmailSender _emailSender;

        public UserServiceImpl(ProductContext productContext,EmailSender emailSender)
        {
            this.productContext = productContext;
            _emailSender = emailSender;
        }
        
        
        public async Task<UserDto> SaveUserAsync(UserDto userDto)
        {
            User user = new User();
            user.UserName = userDto.UserName;
            user.Email = userDto.Email;
            user.Password = userDto.Password;

            // Generate OTP
            var otpCode = OtpGenerator.GenerateOtp();
            var otpExpiry = DateTime.Now.AddMinutes(5);

            user.OtpCode = otpCode;
            user.OtpExpiry = otpExpiry;
            user.IsOtpVerified = false;

            // Send OTP email to the user
            await _emailSender.SendOtpEmailAsync(userDto.Email, otpCode);


            // Save user to the database
            await productContext.Users.AddAsync(user);
            await productContext.SaveChangesAsync();

            // Return the user data as a DTO
            return new UserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password,
                IsOtpVerified = user.IsOtpVerified
                
            };        
            
        }

         public async Task<UserDto> LoginUserAsync(UserDto userDto)
        {
            User user = new User();
            user.UserName = userDto.UserName;
            user.Email = userDto.Email;
            user.Password = userDto.Password;

            // Generate OTP
            var otpCode = OtpGenerator.GenerateOtp();
            var otpExpiry = DateTime.Now.AddMinutes(5);

            user.OtpCode = otpCode;
            user.OtpExpiry = otpExpiry;
            user.IsOtpVerified = false;

            // Send OTP email to the user
            await _emailSender.SendOtpEmailAsync(userDto.Email, otpCode);


            // Save user to the database
            await productContext.Users.AddAsync(user);
            await productContext.SaveChangesAsync();

            // Return the user data as a DTO
            return new UserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password,
                IsOtpVerified = user.IsOtpVerified
                
            };        
            
        }

    
    }
}