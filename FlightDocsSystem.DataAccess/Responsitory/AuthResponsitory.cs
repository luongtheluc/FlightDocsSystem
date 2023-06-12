using System.Runtime.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FlightDocsSystem.DataAccess.Data;
using FlightDocsSystem.DataAccess.Responsitory.IResponsitory;
using FlightDocsSystem.Model;
using FlightDocsSystem.Model.Models;
using FlightDocsSystem.Models;
using FlightDocsSystem.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace FlightDocsSystem.DataAccess.Responsitory
{
    public class AuthResponsitory : IAuthResponsitory
    {
        private readonly FlightDocsSystemContext _context;
        private readonly IMapper _mapper;
        public AuthResponsitory(FlightDocsSystemContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<int> AddUserAsync(UserDTO user, string randomToken)
        {
            if (await _context.Users!.CountAsync(x => x.Email == user.Email) > 0)
            {
                return -1;
            }
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password); //mã hoá password bằng Bycript
            user.Password = passwordHash;

            var addnew = _mapper.Map<User>(user);
            addnew.VerificationToken = randomToken;
            Console.WriteLine("\n \n id cua user: " + addnew.Username + "\n \n");
            _context.Users!.Add(addnew);
            await _context.SaveChangesAsync();
            return addnew.UserId;
        }

        public async Task<User> CheckEmailAsync(string email)
        {
            var user = await _context.Users!.FirstOrDefaultAsync(u => u.Email == email);
            return user!;
        }

        public async Task<User> CheckLoginAsync(string username, string password)
        {
            if (await CheckUserNameAsync(username))
            {
                var user = await _context.Users!.Where(x => x.Username == username).FirstOrDefaultAsync();
                var verify = BCrypt.Net.BCrypt.Verify(password, user!.Password);
                if (verify)
                {
                    return user;
                }
                else
                {
                    return null!;
                }
            }
            return null!;
        }

        public async Task<User> CheckPasswordResetTokenAsync(string token)
        {
            var user = await _context.Users!.FirstOrDefaultAsync(u => u.PasswordResetToken == token);
            return user!;
        }

        public async Task<bool> CheckUserNameAsync(string username)
        {
            var result = await _context.Users!.CountAsync(x => x.Username == username) > 0;
            return result;
        }

        public async Task<User> CheckVerifyTokenAsync(string token)
        {
            var user = await _context.Users!.FirstOrDefaultAsync(u => u.VerificationToken == token);
            return user!;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _context.Users!.FindAsync(id);
            return user!;
        }

        public async Task<string> GetUserRole(User user)
        {
            var roles = await _context.UserRoles!.Where(p => p.UserId == user.UserId).FirstOrDefaultAsync();
            return roles!.Role.RoleName!;
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users!.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}