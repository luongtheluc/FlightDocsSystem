using System;
using System.Collections.Generic;

namespace FlightDocsSystem.Models.DTOs;

public partial class FlightDocumentTypeDTO
{
    public int DocumentTypeId { get; set; }

    public string? DocumentTypeName { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }

}
