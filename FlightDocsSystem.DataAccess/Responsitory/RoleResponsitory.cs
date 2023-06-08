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
    public class RoleResponsitory : IRoleResponsitory
    {
        private readonly FlightDocsSystemContext _context;
        private readonly IMapper _mapper;
        public RoleResponsitory(FlightDocsSystemContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;

        }
        public async Task<int> AddRoleAsync(RoleDTO model)
        {
            var addnew = _mapper.Map<Role>(model);
            _context.Roles!.Add(addnew);
            await _context.SaveChangesAsync();
            return addnew.RoleId;
        }

        public async Task DeleteRoleAsync(int id)
        {
            var delete = await _context.Roles!.Where(p => p.RoleId == id).SingleOrDefaultAsync();
            if (delete != null)
            {
                _context.Roles!.Remove(delete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<RoleDTO> GetRoleByIdAsync(int id)
        {
            var getById = await _context.Roles!.FindAsync(id);
            return _mapper.Map<RoleDTO>(getById);
        }

        public async Task<List<RoleDTO>> GetAllRoleAsync()
        {
            var getAll = await _context.Roles!.ToListAsync();
            return _mapper.Map<List<RoleDTO>>(getAll);
        }

        public async Task UpdateRoleAsync(int id, RoleDTO model)
        {
            if (id == model.RoleId)
            {
                var update = _mapper.Map<Role>(model);
                _context.Roles!.Update(update);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddUserRole(UserRoleDTO userRoleDTO)
        {
            if (userRoleDTO != null)
            {
                var addUserRole = _mapper.Map<UserRole>(userRoleDTO);
                _context.UserRoles!.Add(addUserRole);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetUserRoleAsync()
        {
            var roleid = await _context.Roles!.Where(p => p.RoleName == "User").FirstOrDefaultAsync();
            return roleid!.RoleId;
        }
    }
}