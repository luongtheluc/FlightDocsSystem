using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FlightDocsSystem.DataAccess.Responsitory.IResponsitory;
using FlightDocsSystem.Model;
using FlightDocsSystem.Models;
using FlightDocsSystem.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace FlightDocsSystem.DataAccess.Responsitory
{
    public class PassengerResponsitory : IPassengerResponsitory
    {
        private readonly FlightDocsSystemContext _context;
        private readonly IMapper _mapper;
        public PassengerResponsitory(FlightDocsSystemContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;

        }
        public async Task<int> AddPassengerAsync(PassengerDTO model)
        {
            var addnew = _mapper.Map<Passenger>(model);
            _context.Passengers!.Add(addnew);
            await _context.SaveChangesAsync();
            return addnew.PassengerId;
        }

        public async Task DeletePassengerAsync(int id)
        {
            var delete = await _context.Passengers!.Where(p => p.PassengerId == id).SingleOrDefaultAsync();
            if (delete != null)
            {
                _context.Passengers!.Remove(delete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<PassengerDTO> GetPassengerByIdAsync(int id)
        {
            var getById = await _context.Passengers!.FindAsync(id);
            return _mapper.Map<PassengerDTO>(getById);
        }

        public async Task<List<PassengerDTO>> GetAllPassengerAsync()
        {
            var getAll = await _context.Passengers!.ToListAsync();
            return _mapper.Map<List<PassengerDTO>>(getAll);
        }

        public async Task UpdatePassengerAsync(int id, PassengerDTO model)
        {
            if (id == model.PassengerId)
            {
                var update = _mapper.Map<Passenger>(model);
                _context.Passengers!.Update(update);
                await _context.SaveChangesAsync();
            }
        }
    }
}