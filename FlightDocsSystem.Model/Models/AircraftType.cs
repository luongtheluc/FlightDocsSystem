using System;
using System.Collections.Generic;

namespace FlightDocsSystem.Model.Models;

public partial class AircraftType
{
    public int AircraftTypeId { get; set; }

    public string? AircraftTypeName { get; set; }

    public string? Manufacturer { get; set; }

    public int? SeatingCapacity { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public virtual ICollection<Aircraft> Aircraft { get; set; } = new List<Aircraft>();
}
