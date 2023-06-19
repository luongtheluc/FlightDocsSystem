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
    public class FlightRepository : IFlightRepository
    {
        private readonly FlightDocsSystemContext _context;
        private readonly IMapper _mapper;

        public FlightRepository(IMapper mapper, FlightDocsSystemContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<int> AddFlightAsync(FlightDTO model)
        {
            var addnew = _mapper.Map<Flight>(model);
            _context.Flights!.Add(addnew);
            await _context.SaveChangesAsync();
            return addnew.FlightId;
        }

        public async Task DeleteFlightAsync(int id)
        {
            var delete = await _context.Flights!.Where(p => p.FlightId == id).SingleOrDefaultAsync();
            if (delete != null)
            {
                _context.Flights!.Remove(delete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<FlightDTO>> GetAllFlightAsync()
        {
            var getAll = await _context.Flights!.ToListAsync();
            return _mapper.Map<List<FlightDTO>>(getAll);
        }

        public async Task<FlightDTO> GetFlightByIdAsync(int id)
        {
            var getById = await _context.Flights!.FindAsync(id);
            return _mapper.Map<FlightDTO>(getById);
        }

        public async Task UpdateFlightAsync(int id, FlightDTO model)
        {
            if (id == model.FlightId)
            {
                var update = _mapper.Map<Flight>(model);
                _context.Flights!.Update(update);
                await _context.SaveChangesAsync();
            }
        }
    }
}