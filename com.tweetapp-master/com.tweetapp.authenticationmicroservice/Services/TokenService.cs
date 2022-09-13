using com.tweetapp.authenticationmicroservice.Model;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace com.tweetapp.authenticationmicroservice.Services
{
    public class TokenService:ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        public TokenService(string key)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }

        public string CreateToken(UserAccount userAccount)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, userAccount.UserAccountId),
                new Claim(JwtRegisteredClaimNames.UniqueName, userAccount.UserName),
            };

            SigningCredentials creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);    // creating credentials

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor    // creating token descriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(15),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();    // assigning JwtSequrityTokenHandler to tokenHandler

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);    // creating token with token descriptor

            return tokenHandler.WriteToken(token);    // Returning the token
        }
    }
}
