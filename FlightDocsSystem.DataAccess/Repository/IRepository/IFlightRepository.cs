using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightDocsSystem.Models.DTOs;

namespace FlightDocsSystem.DataAccess.Repository.IRepository
{
    public interface IFlightRepository
    {
        public Task<List<FlightDTO>> GetAllFlightAsync();
        public Task<FlightDTO> GetFlightByIdAsync(int id);
        public Task<int> AddFlightAsync(FlightDTO model);
        public Task UpdateFlightAsync(int id, FlightDTO model);
        public Task DeleteFlightAsync(int id);
    }
}