using System;
using System.Collections.Generic;

namespace FlightDocsSystem.Models;

public partial class AirportDTO
{
    public int AirportId { get; set; }

    public string? AirportCode { get; set; }

    public string? AirportName { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

}
