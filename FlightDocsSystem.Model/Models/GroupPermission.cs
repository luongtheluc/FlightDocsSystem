using System;
using System.Collections.Generic;

namespace FlightDocsSystem.Model.Models;

public partial class GroupPermission
{
    public int GroupId { get; set; }

    public int? DocumentId { get; set; }

    public int? RoleId { get; set; }

    public int? PermissionId { get; set; }

    public string? GroupName { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public virtual Document? Document { get; set; }

    public virtual Permission? Permission { get; set; }

    public virtual Role? Role { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
