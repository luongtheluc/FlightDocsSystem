using System;
using System.Collections.Generic;

namespace FlightDocsSystem.Model.Models;

public partial class Document
{
    public int DocumentId { get; set; }

    public int? FlightId { get; set; }

    public string? DocumentPath { get; set; }

    public string? CoverPath { get; set; }

    public string? DocumentVersion { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public int? DocumentTypeId { get; set; }

    public int? PassengerId { get; set; }

    public int? UserId { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public bool? IsConfirm { get; set; }

    public virtual FlightDocumentType? DocumentType { get; set; }

    public virtual Flight? Flight { get; set; }

    public virtual ICollection<GroupPermission> GroupPermissions { get; set; } = new List<GroupPermission>();

    public virtual Passenger? Passenger { get; set; }

    public virtual User? User { get; set; }
}
