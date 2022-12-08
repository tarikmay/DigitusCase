using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserLoginApp.Entities.Conrete;

namespace UserLoginApp.Business.Helper.JwtToken
{
    public interface ITokenService
    {
        string BuildToken(string key, string issuer, User kullanici,List<Claim> claims);
    }
}
