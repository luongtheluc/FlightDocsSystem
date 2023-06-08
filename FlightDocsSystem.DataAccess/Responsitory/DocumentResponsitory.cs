using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FlightDocsSystem.DataAccess.Data;
using FlightDocsSystem.DataAccess.Responsitory.IResponsitory;
using FlightDocsSystem.Model.Models;
using FlightDocsSystem.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace FlightDocsSystem.DataAccess.Responsitory
{
    public class DocumentResponsitory : IDocumentResponsitory
    {
        private readonly FlightDocsSystemContext _context;
        private readonly IMapper _mapper;

        public DocumentResponsitory(IMapper mapper, FlightDocsSystemContext context)
        {
            _mapper = mapper;
            this._context = context;
        }

        public async Task<int> AddDocumentAsync(DocumentDTO model)
        {
            var addnew = _mapper.Map<Document>(model);
            _context.Documents!.Add(addnew);
            await _context.SaveChangesAsync();
            return addnew.DocumentId;
        }

        public async Task DeleteDocumentAsync(int id)
        {
            var delete = await _context.Documents!.Where(p => p.DocumentId == id).SingleOrDefaultAsync();
            if (delete != null)
            {
                _context.Documents!.Remove(delete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<DocumentDTO>> GetAllDocumentAsync()
        {
            var getAll = await _context.Documents!.ToListAsync();
            return _mapper.Map<List<DocumentDTO>>(getAll);
        }

        public async Task<DocumentDTO> GetDocumentByIdAsync(int id)
        {
            var getById = await _context.Documents!.FindAsync(id);
            return _mapper.Map<DocumentDTO>(getById);
        }

        public async Task UpdateDocumentAsync(int id, DocumentDTO model)
        {
            if (id == model.DocumentId)
            {
                var update = _mapper.Map<Document>(model);
                _context.Documents!.Update(update);
                await _context.SaveChangesAsync();
            }
        }
    }
}