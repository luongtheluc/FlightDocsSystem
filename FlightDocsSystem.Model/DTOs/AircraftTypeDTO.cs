using System;
using System.Collections.Generic;

namespace FlightDocsSystem.Models.DTOs;

public partial class AircraftTypeDTO
{
    public int AircraftTypeId { get; set; }

    public string? AircraftTypeName { get; set; }

    public string? Manufacturer { get; set; }

    public int? SeatingCapacity { get; set; }
}
