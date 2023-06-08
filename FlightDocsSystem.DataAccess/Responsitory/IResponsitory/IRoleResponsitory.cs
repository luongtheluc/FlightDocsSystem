using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightDocsSystem.Models.DTOs;

namespace FlightDocsSystem.DataAccess.Responsitory.IResponsitory
{
    public interface IRoleResponsitory
    {
        public Task<List<RoleDTO>> GetAllRoleAsync();
        public Task<RoleDTO> GetRoleByIdAsync(int id);
        public Task<int> AddRoleAsync(RoleDTO model);
        public Task UpdateRoleAsync(int id, RoleDTO model);
        public Task DeleteRoleAsync(int id);
        public Task AddUserRole(UserRoleDTO userRoleDTO);
        public Task<int> GetUserRoleAsync();
    }
}