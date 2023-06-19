using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FlightDocsSystem.DataAccess.Data;
using FlightDocsSystem.DataAccess.Repository.IRepository;
using FlightDocsSystem.Model.Models;
using FlightDocsSystem.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace FlightDocsSystem.DataAccess.Repository
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly FlightDocsSystemContext _context;
        private readonly IMapper _mapper;

        public DocumentRepository(IMapper mapper, FlightDocsSystemContext context)
        {
            _mapper = mapper;
            _context = context;
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

        public async Task<List<DocumentDTO>> GetAllDocumentAsync(int currentPage = 1, int pageSize = 5, string? searchKeyword = null)
        {
            IQueryable<Document> query = _context.Documents!;

            if (!string.IsNullOrEmpty(searchKeyword))
            {
                query = query.Where(d => d.DocumentName!.Contains(searchKeyword));
            }

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var documents = await query
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return _mapper.Map<List<DocumentDTO>>(documents);
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