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
    public class FlightDocumentTypeResponsitory : IFlightDocumentTypeResponsitory
    {
        private readonly FlightDocsSystemContext _context;
        private readonly IMapper _mapper;

        public FlightDocumentTypeResponsitory(IMapper mapper, FlightDocsSystemContext context)
        {
            _mapper = mapper;
            this._context = context;
        }

        public async Task<int> AddFlightDocumentTypeAsync(FlightDocumentTypeDTO model)
        {
            var addnew = _mapper.Map<FlightDocumentType>(model);
            _context.FlightDocumentTypes!.Add(addnew);
            await _context.SaveChangesAsync();
            return addnew.DocumentTypeId;
        }

        public async Task DeleteFlightDocumentTypeAsync(int id)
        {
            var delete = await _context.FlightDocumentTypes!.Where(p => p.DocumentTypeId == id).SingleOrDefaultAsync();
            if (delete != null)
            {
                _context.FlightDocumentTypes!.Remove(delete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<FlightDocumentTypeDTO>> GetAllFlightDocumentTypeAsync()
        {
            var getAll = await _context.FlightDocumentTypes!.ToListAsync();
            return _mapper.Map<List<FlightDocumentTypeDTO>>(getAll);
        }

        public async Task<FlightDocumentTypeDTO> GetFlightDocumentTypeByIdAsync(int id)
        {
            var getById = await _context.FlightDocumentTypes!.FindAsync(id);
            return _mapper.Map<FlightDocumentTypeDTO>(getById);
        }

        public async Task UpdateFlightDocumentTypeAsync(int id, FlightDocumentTypeDTO model)
        {
            if (id == model.DocumentTypeId)
            {
                var update = _mapper.Map<FlightDocumentType>(model);
                _context.FlightDocumentTypes!.Update(update);
                await _context.SaveChangesAsync();
            }
        }
    }
}