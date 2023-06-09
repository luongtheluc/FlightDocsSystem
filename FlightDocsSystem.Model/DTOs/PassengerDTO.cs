﻿using System;
using System.Collections.Generic;

namespace FlightDocsSystem.Models.DTOs;

public partial class PassengerDTO
{

    public int PassengerId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? Gender { get; set; }

}
