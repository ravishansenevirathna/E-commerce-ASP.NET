using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace EcommerceApi.Config.Auth
{
    public class JwtHelper
    {
    private readonly string _key;
    private readonly string _issuer;

    public JwtHelper(IConfiguration configuration)
    {
        _key = configuration["Jwt:Key"];
        _issuer = configuration["Jwt:Issuer"];
    }

    public string GenerateToken(string username) // Renamed to GenerateToken
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourVerySecureLongKey1234567890123456"));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
}