using System;
using System.Collections.Generic;

namespace FlightDocsSystem.Model.DTOs;

public partial class GroupPermissionDTO
{
    public int GroupId { get; set; }

    public int? DocumentId { get; set; }

    public int? RoleId { get; set; }

    public int? PermissionId { get; set; }

    public string? GroupName { get; set; }


}

