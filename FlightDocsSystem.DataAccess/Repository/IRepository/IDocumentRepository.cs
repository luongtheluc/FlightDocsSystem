using System.Reflection.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightDocsSystem.Models.DTOs;

namespace FlightDocsSystem.DataAccess.Repository.IRepository
{
    public interface IDocumentRepository
    {
        public Task<List<DocumentDTO>> GetAllDocumentAsync(int currentPage, int pageSize, string? searchKeyword = null);
        public Task<DocumentDTO> GetDocumentByIdAsync(int id);
        public Task<int> AddDocumentAsync(DocumentDTO model);
        public Task UpdateDocumentAsync(int id, DocumentDTO model);
        public Task DeleteDocumentAsync(int id);
    }
}