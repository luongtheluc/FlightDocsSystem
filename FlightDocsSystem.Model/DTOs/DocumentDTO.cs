using System;
using System.Collections.Generic;

namespace FlightDocsSystem.Models;

public partial class DocumentDTO
{
    public int DocumentId { get; set; }

    public int? FlightId { get; set; }

    public string? DocumentType { get; set; }

    public string? DocumentNumber { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public int? DocumentTypeId { get; set; }

    public int? PassengerId { get; set; }

    public int? UserId { get; set; }
}
