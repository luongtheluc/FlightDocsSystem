using System;
using System.Collections.Generic;

namespace FlightDocsSystem.Models;

public partial class TicketDTO
{
    public int TicketId { get; set; }

    public int? FlightId { get; set; }

    public int? PassengerId { get; set; }

    public string? TicketNumber { get; set; }

    public string? SeatNumber { get; set; }


}
