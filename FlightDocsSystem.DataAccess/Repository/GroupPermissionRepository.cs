using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FlightDocsSystem.DataAccess.Data;
using FlightDocsSystem.DataAccess.Repository.IRepository;
using FlightDocsSystem.Model.DTOs;
using FlightDocsSystem.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightDocsSystem.DataAccess.Repository
{
    public class GroupPermissionRepository : IGroupPermissionRepository
    {
        private readonly FlightDocsSystemContext _context;
        private readonly IMapper _mapper;

        public GroupPermissionRepository(IMapper mapper, FlightDocsSystemContext context)
        {
            _mapper = mapper;
            _context = context;

        }


        public async Task<int> AddGroupPermissionAsync(GroupPermissionDTO model)
        {
            var addnew = _mapper.Map<GroupPermission>(model);
            _context.GroupPermissions!.Add(addnew);
            await _context.SaveChangesAsync();
            return addnew.GroupId;
        }

        public async Task DeleteGroupPermissionAsync(int id)
        {
            var delete = await _context.GroupPermissions!.Where(p => p.GroupId == id).SingleOrDefaultAsync();
            if (delete != null)
            {
                _context.GroupPermissions!.Remove(delete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<GroupPermissionDTO>> GetAllGroupPermissionAsync()
        {
            var getAll = await _context.GroupPermissions!.ToListAsync();
            return _mapper.Map<List<GroupPermissionDTO>>(getAll);
        }

        public async Task<GroupPermissionDTO> GetGroupPermissionByIdAsync(int id)
        {
            var getById = await _context.GroupPermissions!.FindAsync(id);
            return _mapper.Map<GroupPermissionDTO>(getById);
        }

        public async Task UpdateGroupPermissionAsync(int id, GroupPermissionDTO model)
        {
            if (id == model.GroupId)
            {
                var update = _mapper.Map<GroupPermission>(model);
                _context.GroupPermissions!.Update(update);
                await _context.SaveChangesAsync();
            }
        }
    }
}