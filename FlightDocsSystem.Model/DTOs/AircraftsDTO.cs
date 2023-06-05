using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDocsSystem.Model.DTOs
{
    public class AircraftsDTO
    {
        public int AircraftId { get; set; }

        public string? AircraftNumber { get; set; }

        public int? AircraftTypeId { get; set; }
    }
}
