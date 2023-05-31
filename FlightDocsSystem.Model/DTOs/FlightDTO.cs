using System;
using System.Collections.Generic;

namespace FlightDocsSystem.Models.DTOs;

public partial class FlightDTO
{
    public int FlightId { get; set; }

    public string? FlightNumber { get; set; }

    public int? DepartureAirportId { get; set; }

    public int? ArrivalAirportId { get; set; }

    public DateTime? DepartureTime { get; set; }

    public DateTime? ArrivalTime { get; set; }

    public int? AircraftId { get; set; }

    public int? UserId { get; set; }

}
