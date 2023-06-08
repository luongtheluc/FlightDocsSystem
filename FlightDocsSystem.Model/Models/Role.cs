using System;
using System.Collections.Generic;

namespace FlightDocsSystem.Model.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string? RoleName { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }
}
