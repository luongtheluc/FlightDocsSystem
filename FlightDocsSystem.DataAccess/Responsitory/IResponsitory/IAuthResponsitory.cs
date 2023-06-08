using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightDocsSystem.Model.Models;
using FlightDocsSystem.Models;
using FlightDocsSystem.Models.DTOs;

namespace FlightDocsSystem.DataAccess.Responsitory.IResponsitory
{
    public interface IAuthResponsitory
    {
        public Task<int> AddUserAsync(UserDTO user);
        public Task<User> GetUserByIdAsync(int id);
        public Task<bool> CheckUserNameAsync(string username);
        public Task<User> CheckLoginAsync(string password, string username);
        public Task<string> GetUserRole(User user);
    }
}