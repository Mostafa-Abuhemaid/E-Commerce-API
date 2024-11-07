using E_Commerce.Core.Identity;
using E_Commerce.Core.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> CreateToken(ApplicationUser user)
        {
           var AuthClaims = new List<Claim>()
           { new Claim(ClaimTypes.NameIdentifier, user.Id),
               new Claim(ClaimTypes.GivenName, user.Name),
               new Claim(ClaimTypes.Email, user.Email)

           };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                //audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:DurationInDay"])),
                claims:AuthClaims,
                signingCredentials:new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)

                ) ;
            return (new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
