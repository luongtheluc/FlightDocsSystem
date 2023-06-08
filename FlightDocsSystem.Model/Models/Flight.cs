using System;
using System.Collections.Generic;

namespace FlightDocsSystem.Model.Models;

public partial class Flight
{
    public int FlightId { get; set; }

    public string? FlightNumber { get; set; }

    public int? DepartureAirportId { get; set; }

    public int? ArrivalAirportId { get; set; }

    public DateTime? DepartureTime { get; set; }

    public DateTime? ArrivalTime { get; set; }

    public int? AircraftId { get; set; }

    public int? UserId { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public virtual Aircraft? Aircraft { get; set; }

    public virtual Airport? ArrivalAirport { get; set; }

    public virtual Airport? DepartureAirport { get; set; }

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    public virtual User? User { get; set; }
}
