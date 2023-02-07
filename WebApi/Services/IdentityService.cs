using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Authentication;

namespace WebApi.Services
{
    public class IdentityService : IIdentityService
    {
        string signinKey = "this is my custom Secret key for authentication";
        public string Login(string userName, string password)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,userName),
                new Claim(JwtRegisteredClaimNames.Email,userName)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signinKey));
            var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: "https://www.eymenbatinyaci.com",
                audience: "tokenKeyValue",
                claims: claims,
                expires: DateTime.Now.AddDays(5),
                notBefore: DateTime.Now,
                signingCredentials: credential
                );
            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return token;

        }

        public bool ValidateToken(string token)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signinKey));

                JwtSecurityTokenHandler handler = new();
                handler.ValidateToken(token, new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = securityKey,
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidateIssuer = false
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
