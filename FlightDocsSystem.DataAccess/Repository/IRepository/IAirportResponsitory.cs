using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightDocsSystem.Models.DTOs;

namespace FlightDocsSystem.DataAccess.Repository.IRepository
{
    public interface IAirportResponsitory
    {
        public Task<List<AirportDTO>> GetAllAirportAsync();
        public Task<AirportDTO> GetAirportByIdAsync(int id);
        public Task<int> AddAirportAsync(AirportDTO model);
        public Task UpdateAirportAsync(int id, AirportDTO model);
        public Task DeleteAirportAsync(int id);
    }
}