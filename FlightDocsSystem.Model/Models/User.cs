using System;
using System.Collections.Generic;

namespace FlightDocsSystem.Model.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Address { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public bool? IsActive { get; set; }

    public string? UserImage { get; set; }

    public string? PasswordResetToken { get; set; }

    public DateTime? ResetTokenExpries { get; set; }

    public string? VerificationToken { get; set; }

    public DateTime? VerifyAt { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime? RefreshTokenCreated { get; set; }

    public DateTime? RefreshTokenExpries { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    public virtual ICollection<Flight> Flights { get; set; } = new List<Flight>();
}
