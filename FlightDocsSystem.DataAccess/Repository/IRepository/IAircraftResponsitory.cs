using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightDocsSystem.Model.DTOs;

namespace FlightDocsSystem.DataAccess.Repository.IRepository
{
    public interface IAircraftResponsitory
    {
        public Task<List<AircraftsDTO>> GetAllAircraftsAsync();
        public Task<AircraftsDTO> GetAircraftsAsync(int id);
        public Task<int> AddAircraftsAsync(AircraftsDTO model);
        public Task UpdateAircraftsAsync(int id, AircraftsDTO model);
        public Task DeleteAircraftsAsync(int id);

    }
}
