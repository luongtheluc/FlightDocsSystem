using System;
using System.Collections.Generic;

namespace FlightDocsSystem.Model.Models;

public partial class Aircraft
{
    public int AircraftId { get; set; }

    public string? AircraftNumber { get; set; }

    public int? AircraftTypeId { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public virtual AircraftType? AircraftType { get; set; }

    public virtual ICollection<Flight> Flights { get; set; } = new List<Flight>();
}
