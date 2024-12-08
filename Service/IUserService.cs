using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApi.Dto;

namespace EcommerceApi.Service
{
    public interface IUserService
    {
        Task<UserDto> SaveUserAsync(UserDto userDto);
        Task<UserDto> ValidateOtp(UserDto userDto);
        Task<UserDto> LoginRequest(UserDto userDto);
        


    }
}