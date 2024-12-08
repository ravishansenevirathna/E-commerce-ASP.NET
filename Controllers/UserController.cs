using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcommerceApi.Config.Auth;
using EcommerceApi.Dto;
using EcommerceApi.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
       private readonly IUserService userService;

    public UserController(IUserService userService)
    {
        this.userService = userService;
    }  

    [HttpPost("signup")]
    public async Task<ActionResult<UserDto>> SignUpRequest(UserDto userDto){
    {
        var userDto1 = await userService.SaveUserAsync(userDto);
        return CreatedAtAction(nameof(SignUpRequest), new { id = userDto1.Id }, userDto1);
    }

    }

    [HttpPost("validate-otp")]
    public async Task<ActionResult<UserDto>> ValidateOtp(UserDto userDto){
    
        var userDto1 = await userService.ValidateOtp(userDto);
        return CreatedAtAction(nameof(ValidateOtp), new { id = userDto1.Id }, userDto1);
    
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> LoginRequest(UserDto userDto){
    {
        var userDto1 = await userService.LoginRequest(userDto);
        return CreatedAtAction(nameof(LoginRequest), new { id = userDto1.Id }, userDto1);
    }

    }

}


}