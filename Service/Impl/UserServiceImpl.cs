using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApi.Config;
using EcommerceApi.Config.Auth;
using EcommerceApi.Dto;
using EcommerceApi.Models;
using Microsoft.EntityFrameworkCore;

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
            var otpExpiry = DateTime.Now;

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

        public async Task<UserDto> ValidateOtp(UserDto userDto)
        {
            // Fetch the user from the database using the UserId or other unique identifier
            var user = await productContext.Users.FirstOrDefaultAsync(u => u.Email == userDto.Email && u.Password == userDto.Password);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            // Check if the OTP matches
            if (user.OtpCode != userDto.OtpCode)
            {
                throw new Exception("Invalid OTP.");
            }

          TimeSpan timeDifference = DateTime.Now - user.OtpExpiry;

            // Check if the OTP was created more than 5 minutes ago
            if (timeDifference.TotalMinutes < 6)
            {
                throw new Exception("OTP has expired.");
            }

            user.IsOtpVerified = true;

            // Save the changes to the database
            productContext.Users.Update(user);
            await productContext.SaveChangesAsync();

            // Return the updated UserDto
            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                IsOtpVerified = user.IsOtpVerified
            };
        }

        
      public async Task<UserDto> LoginRequest(UserDto userDto)
        {
            // Check if OTP verification is complete
            if (!userDto.IsOtpVerified)
            {
                throw new UnauthorizedAccessException("Unauthorized user. OTP verification required.");
            }

            // Find the user in the database by username or email
            var user = await productContext.Users.FirstOrDefaultAsync(u => u.UserName == userDto.UserName && u.Email == userDto.Email);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            // Validate the password
            if (user.Password != userDto.Password)
            {
                throw new Exception("Invalid username or password.");
            }

            
            return new UserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                IsOtpVerified = user.IsOtpVerified
            };
        }
    
    }
}