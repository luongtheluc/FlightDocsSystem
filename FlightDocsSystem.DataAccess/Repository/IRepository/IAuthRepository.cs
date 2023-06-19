using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightDocsSystem.Model.Models;
using FlightDocsSystem.Models;
using FlightDocsSystem.Models.DTOs;

namespace FlightDocsSystem.DataAccess.Repository.IRepository
{
    public interface IAuthRepository
    {
        public Task<int> AddUserAsync(UserDTO user, string randomToken);
        public Task<User> GetUserByIdAsync(int id);
        public Task<User> CheckVerifyTokenAsync(string token);
        public Task<bool> CheckUserNameAsync(string username);
        public Task<User> CheckLoginAsync(string password, string username);
        public Task UpdateUserAsync(User user);
        Task<User> CheckEmailAsync(string email);
        Task<User> CheckPasswordResetTokenAsync(string token);
        public Task<int> CheckRefreshToken(string token);
        Task<User> GetUserByRefreshToken(string token);
        Task<User> GetUserByAccessToken(string token, string secretKey);
    }
}