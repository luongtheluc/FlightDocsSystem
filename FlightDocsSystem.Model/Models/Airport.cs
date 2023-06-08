using System;
using System.Collections.Generic;

namespace FlightDocsSystem.Model.Models;

public partial class Airport
{
    public int AirportId { get; set; }

    public string? AirportCode { get; set; }

    public string? AirportName { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public virtual ICollection<Flight> FlightArrivalAirports { get; set; } = new List<Flight>();

    public virtual ICollection<Flight> FlightDepartureAirports { get; set; } = new List<Flight>();
}
