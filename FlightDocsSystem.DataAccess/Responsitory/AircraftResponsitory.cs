using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FlightDocsSystem.DataAccess.Responsitory.IResponsitory;
using FlightDocsSystem.Model;
using FlightDocsSystem.Model.DTOs;
using FlightDocsSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightDocsSystem.DataAccess.Responsitory
{
    public class AircraftResponsitory : IAircraftResponsitory
    {
        private readonly FlightDocsSystemContext _context;
        private readonly IMapper _mapper;

        public AircraftResponsitory(FlightDocsSystemContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;

        }

        public async Task<int> AddAircraftsAsync(AircraftsDTO model)
        {
            var addnew = _mapper.Map<Aircraft>(model);
            _context.Aircrafts!.Add(addnew);
            await _context.SaveChangesAsync();
            return addnew.AircraftId;
        }

        public async Task DeleteAircraftsAsync(int id)
        {
            var delete = await _context.Aircrafts!.Where(p => p.AircraftId == id).SingleOrDefaultAsync();
            if (delete != null)
            {
                _context.Aircrafts!.Remove(delete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<AircraftsDTO> GetAircraftsAsync(int id)
        {
            var getById = await _context.Aircrafts!.FindAsync(id);
            return _mapper.Map<AircraftsDTO>(getById);
        }

        public async Task<List<AircraftsDTO>> GetAllAircraftsAsync()
        {
            var getAll = await _context.Aircrafts!.ToListAsync();
            return _mapper.Map<List<AircraftsDTO>>(getAll);
        }

        public async Task UpdateAircraftsAsync(int id, AircraftsDTO model)
        {
            if (id == model.AircraftId)
            {
                var update = _mapper.Map<Aircraft>(model);
                _context.Aircrafts!.Update(update);
                await _context.SaveChangesAsync();
            }
        }
    }
}