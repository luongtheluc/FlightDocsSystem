using System;
using System.Collections.Generic;

namespace FlightDocsSystem.Models.DTOs;

public partial class DocumentDTO
{
    public int DocumentId { get; set; }

    public int? FlightId { get; set; }

    public string? DocumentPath { get; set; }
    public string? CoverPath { get; set; }

    public string? DocumentVersion { get; set; }
    
    public bool? IsComfirm { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public int? DocumentTypeId { get; set; }

    public int? PassengerId { get; set; }

    public int? UserId { get; set; }
}
