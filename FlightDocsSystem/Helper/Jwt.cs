using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Azure;
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
                new Claim(ClaimTypes.Role, role ),
                new Claim(ClaimTypes.Email, user.Email!),
            };
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

        public static RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expired = DateTime.Now.AddDays(7),

            };
            return refreshToken;
        }

    }

    public class RefreshToken
    {
        public string? Token { get; set; }
        public DateTime Created { get; set; }
        public DateTime Expired { get; set; }

    }
}