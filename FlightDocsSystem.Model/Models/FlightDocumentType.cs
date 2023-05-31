using System;
using System.Collections.Generic;

namespace FlightDocsSystem.Models;

public partial class FlightDocumentType
{
    public int DocumentTypeId { get; set; }

    public string? DocumentTypeName { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();
}
