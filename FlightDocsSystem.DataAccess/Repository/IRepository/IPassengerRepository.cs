using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightDocsSystem.Models.DTOs;

namespace FlightDocsSystem.DataAccess.Repository.IRepository
{
    public interface IPassengerRepository
    {
        public Task<List<PassengerDTO>> GetAllPassengerAsync();
        public Task<PassengerDTO> GetPassengerByIdAsync(int id);
        public Task<int> AddPassengerAsync(PassengerDTO model);
        public Task UpdatePassengerAsync(int id, PassengerDTO model);
        public Task DeletePassengerAsync(int id);
    }
}