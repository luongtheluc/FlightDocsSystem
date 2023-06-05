using System;
using System.Collections.Generic;

namespace FlightDocsSystem.Models.DTOs;

public partial class RoleDTO
{
    public int RoleId { get; set; }

    public string? RoleName { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }
}
