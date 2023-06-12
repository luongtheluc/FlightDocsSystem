using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using FlightDocsSystem.Model.Models;
using Microsoft.IdentityModel.Tokens;

namespace FlightDocsSystem.Helper
{
    public class Jwt
    {

        public static string CreateToken(User user, string secretKey, string role = "User")
        {
            List<Claim> claims = new List<Claim>{
                new Claim(ClaimTypes.Name, user.Username!),
                new Claim(ClaimTypes.Role, role )
            };
            // var secretKey = "your_secret_key_here";
            // var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds

            );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        public static string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }
    }
}