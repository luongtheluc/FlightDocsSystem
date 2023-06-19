using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightDocsSystem.Model.DTOs;
using FlightDocsSystem.Models.DTOs;

namespace FlightDocsSystem.DataAccess.Repository.IRepository
{
    public interface IAircraftTypeRepository
    {
        public Task<List<AircraftTypeDTO>> GetAllAircraftTypeAsync();
        public Task<AircraftTypeDTO> GetAircraftTypeByIdAsync(int id);
        public Task<int> AddAircraftTypeAsync(AircraftTypeDTO model);
        public Task UpdateAircraftTypeAsync(int id, AircraftTypeDTO model);
        public Task DeleteAircraftTypeAsync(int id);

    }
}
