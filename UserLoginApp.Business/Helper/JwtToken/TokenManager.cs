using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using UserLoginApp.Entities.Conrete;

namespace UserLoginApp.Business.Helper.JwtToken
{
    public class TokenManager:ITokenService
    {
        private const double EXPIRY_DURATION_MINUTES = 30;
        public string BuildToken(string key, string issuer, User user,List<Claim> claims)
        {
           
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims,
                expires: DateTime.Now.AddMinutes(EXPIRY_DURATION_MINUTES), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

       
    }
}

