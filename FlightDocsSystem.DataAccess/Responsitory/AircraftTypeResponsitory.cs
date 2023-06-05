using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FlightDocsSystem.DataAccess.Responsitory.IResponsitory;
using FlightDocsSystem.Model;
using FlightDocsSystem.Model.DTOs;
using FlightDocsSystem.Models;
using FlightDocsSystem.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace FlightDocsSystem.DataAccess.Responsitory
{
    public class AircraftTypesResponsitory : IAircraftTypeResponsitory
    {
        private readonly FlightDocsSystemContext _context;
        private readonly IMapper _mapper;

        public AircraftTypesResponsitory(FlightDocsSystemContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;

        }

        public async Task<int> AddAircraftTypeAsync(AircraftTypeDTO model)
        {
            var addnew = _mapper.Map<AircraftType>(model);
            _context.AircraftTypes!.Add(addnew);
            await _context.SaveChangesAsync();
            return addnew.AircraftTypeId;
        }

        public async Task DeleteAircraftTypeAsync(int id)
        {
            var delete = await _context.AircraftTypes!.Where(p => p.AircraftTypeId == id).SingleOrDefaultAsync();
            if (delete != null)
            {
                _context.AircraftTypes!.Remove(delete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<AircraftTypeDTO> GetAircraftTypeByIdAsync(int id)
        {
            var getById = await _context.AircraftTypes!.FindAsync(id);
            return _mapper.Map<AircraftTypeDTO>(getById);
        }

        public async Task<List<AircraftTypeDTO>> GetAllAircraftTypeAsync()
        {
            var getAll = await _context.AircraftTypes!.ToListAsync();
            return _mapper.Map<List<AircraftTypeDTO>>(getAll);
        }



        public async Task UpdateAircraftTypeAsync(int id, AircraftTypeDTO model)
        {
            if (id == model.AircraftTypeId)
            {
                var update = _mapper.Map<AircraftType>(model);
                _context.AircraftTypes!.Update(update);
                await _context.SaveChangesAsync();
            }
        }

    }
}