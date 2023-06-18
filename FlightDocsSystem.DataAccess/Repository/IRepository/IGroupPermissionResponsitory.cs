using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightDocsSystem.Model.DTOs;

namespace FlightDocsSystem.DataAccess.Repository.IRepository
{   
    public interface IGroupPermissionResponsitory
    {
        public Task<List<GroupPermissionDTO>> GetAllGroupPermissionAsync();
        public Task<GroupPermissionDTO> GetGroupPermissionByIdAsync(int id);
        public Task<int> AddGroupPermissionAsync(GroupPermissionDTO model);
        public Task UpdateGroupPermissionAsync(int id, GroupPermissionDTO model);
        public Task DeleteGroupPermissionAsync(int id);
    }
}