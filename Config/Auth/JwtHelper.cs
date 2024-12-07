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

    public string GenerateToken(string username, List<string> roles)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username)
        };

        roles.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _issuer,
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
}