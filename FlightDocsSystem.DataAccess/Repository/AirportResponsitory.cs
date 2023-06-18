using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FlightDocsSystem.DataAccess.Data;
using FlightDocsSystem.DataAccess.Repository.IRepository;
using FlightDocsSystem.Model;
using FlightDocsSystem.Model.Models;
using FlightDocsSystem.Models;
using FlightDocsSystem.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace FlightDocsSystem.DataAccess.Repository
{
    public class AirportResponsitory : IAirportResponsitory
    {
        private readonly FlightDocsSystemContext _context;
        private readonly IMapper _mapper;
        public AirportResponsitory(FlightDocsSystemContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        public async Task<int> AddAirportAsync(AirportDTO model)
        {
            var addnew = _mapper.Map<Airport>(model);
            _context.Airports!.Add(addnew);
            await _context.SaveChangesAsync();
            return addnew.AirportId;
        }

        public async Task DeleteAirportAsync(int id)
        {
            var delete = await _context.Airports!.Where(p => p.AirportId == id).SingleOrDefaultAsync();
            if (delete != null)
            {
                _context.Airports!.Remove(delete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<AirportDTO> GetAirportByIdAsync(int id)
        {
            var getById = await _context.Airports!.FindAsync(id);
            return _mapper.Map<AirportDTO>(getById);
        }

        public async Task<List<AirportDTO>> GetAllAirportAsync()
        {
            var getAll = await _context.Airports!.ToListAsync();
            return _mapper.Map<List<AirportDTO>>(getAll);
        }

        public async Task UpdateAirportAsync(int id, AirportDTO model)
        {
            if (id == model.AirportId)
            {
                var update = _mapper.Map<Airport>(model);
                _context.Airports!.Update(update);
                await _context.SaveChangesAsync();
            }
        }
    }
}