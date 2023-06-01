using System;
using System.Collections.Generic;
namespace FlightDocsSystem.Models;

public partial class Passenger
{
    public int PassengerId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
