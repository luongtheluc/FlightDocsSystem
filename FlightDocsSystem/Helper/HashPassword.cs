using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightDocsSystem.Helper
{
    public class HashPassword
    {
        public static string HashPasswordBycript(string password)
        {
            var hashPassword = BCrypt.Net.BCrypt.HashPassword(password);
            return hashPassword;
        }
    }
}