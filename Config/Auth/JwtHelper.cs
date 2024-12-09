// using System;
// using System.Collections.Generic;
// using System.IdentityModel.Tokens.Jwt;
// using System.Linq;
// using System.Security.Claims;
// using System.Text;
// using System.Threading.Tasks;
// using Microsoft.IdentityModel.Tokens;

// namespace EcommerceApi.Config.Auth
// {
//     public class JwtHelper
//     {
//     private readonly string _key;
//     private readonly string _issuer;

//     public JwtHelper(IConfiguration configuration)
//     {
//         _key = configuration["Jwt:Key"]; // Use the key from the configuration
//         _issuer = configuration["Jwt:Issuer"];
//     }

//     public string GenerateToken(string username)
//     {
//         var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));  // Use _key from configuration
//         var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

//         var claims = new[] 
//         {
//             new Claim(JwtRegisteredClaimNames.Sub, username),
//             new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
//         };

//         var token = new JwtSecurityToken(
//             issuer: _issuer,
//             audience: _issuer, // Can set audience if needed
//             claims: claims,
//             expires: DateTime.UtcNow.AddHours(1),
//             signingCredentials: credentials
//         );

//         return new JwtSecurityTokenHandler().WriteToken(token);
//     }

//     public ClaimsPrincipal ValidateToken(string token)
//     {
//         var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));  // Use the same key

//         var tokenHandler = new JwtSecurityTokenHandler();
//         try
//         {
//             var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
//             {
//                 IssuerSigningKey = securityKey,
//                 ValidIssuer = _issuer,
//                 ValidAudience = _issuer,
//                 ClockSkew = TimeSpan.Zero // Optional: allow zero clock skew for exact expiration match
//             }, out var validatedToken);

//             return principal; // Returns validated ClaimsPrincipal
//         }
//         catch (Exception)
//         {
//             return null; // Invalid token or signature
//         }
//     }
// }
// }