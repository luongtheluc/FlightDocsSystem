using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightDocsSystem.Models.DTOs;

namespace FlightDocsSystem.DataAccess.Repository.IRepository
{
    public interface IFlightDocumentTypeResponsitory
    {
        public Task<List<FlightDocumentTypeDTO>> GetAllFlightDocumentTypeAsync();
        public Task<FlightDocumentTypeDTO> GetFlightDocumentTypeByIdAsync(int id);
        public Task<int> AddFlightDocumentTypeAsync(FlightDocumentTypeDTO model);
        public Task UpdateFlightDocumentTypeAsync(int id, FlightDocumentTypeDTO model);
        public Task DeleteFlightDocumentTypeAsync(int id);
    }
}