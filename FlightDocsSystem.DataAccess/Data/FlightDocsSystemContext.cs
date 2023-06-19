using System;
using System.Collections.Generic;
using FlightDocsSystem.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightDocsSystem.DataAccess.Data;

public partial class FlightDocsSystemContext : DbContext
{
    public FlightDocsSystemContext()
    {
    }

    public FlightDocsSystemContext(DbContextOptions<FlightDocsSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aircraft>? Aircrafts { get; set; }

    public virtual DbSet<AircraftType>? AircraftTypes { get; set; }

    public virtual DbSet<Airport>? Airports { get; set; }

    public virtual DbSet<Document>? Documents { get; set; }

    public virtual DbSet<Flight>? Flights { get; set; }

    public virtual DbSet<FlightDocumentType>? FlightDocumentTypes { get; set; }

    public virtual DbSet<GroupPermission>? GroupPermissions { get; set; }

    public virtual DbSet<Passenger>? Passengers { get; set; }

    public virtual DbSet<Permission>? Permissions { get; set; }

    public virtual DbSet<Role>? Roles { get; set; }

    public virtual DbSet<User>? Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("Data Source=THELUC;Initial Catalog=FlightDocsSystem;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aircraft>(entity =>
        {
            entity.HasKey(e => e.AircraftId).HasName("PK__Aircraft__04015399FA3E6786");

            entity.Property(e => e.AircraftId).HasColumnName("aircraft_id");
            entity.Property(e => e.AircraftNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("aircraft_number");
            entity.Property(e => e.AircraftTypeId).HasColumnName("aircraft_type_id");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("update_at");

            entity.HasOne(d => d.AircraftType).WithMany(p => p.Aircraft)
                .HasForeignKey(d => d.AircraftTypeId)
                .HasConstraintName("FK_Aircrafts_AircraftTypes");
        });

        modelBuilder.Entity<AircraftType>(entity =>
        {
            entity.Property(e => e.AircraftTypeId).HasColumnName("aircraft_type_id");
            entity.Property(e => e.AircraftTypeName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("aircraft_type_name");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.Manufacturer)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("manufacturer");
            entity.Property(e => e.SeatingCapacity).HasColumnName("seating_capacity");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("update_at");
        });

        modelBuilder.Entity<Airport>(entity =>
        {
            entity.HasKey(e => e.AirportId).HasName("PK__Airports__C795D516759AC957");

            entity.Property(e => e.AirportId).HasColumnName("airport_id");
            entity.Property(e => e.AirportCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("airport_code");
            entity.Property(e => e.AirportName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("airport_name");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("country");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("update_at");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.DocumentId).HasName("PK__Document__9666E8AC859AE755");

            entity.Property(e => e.DocumentId).HasColumnName("document_id");
            entity.Property(e => e.CoverPath).HasColumnName("cover_path");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.DocumentPath).HasColumnName("document_path");
            entity.Property(e => e.DocumentName).HasColumnName("document_name");
            entity.Property(e => e.DocumentTypeId).HasColumnName("document_type_id");
            entity.Property(e => e.DocumentVersion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("document_version");
            entity.Property(e => e.ExpirationDate)
                .HasColumnType("date")
                .HasColumnName("expiration_date");
            entity.Property(e => e.FlightId).HasColumnName("flight_id");
            entity.Property(e => e.PassengerId).HasColumnName("passenger_id");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("update_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.DocumentType).WithMany(p => p.Documents)
                .HasForeignKey(d => d.DocumentTypeId)
                .HasConstraintName("FK_Documents_FlightDocumentTypes");

            entity.HasOne(d => d.Flight).WithMany(p => p.Documents)
                .HasForeignKey(d => d.FlightId)
                .HasConstraintName("FK__Documents__fligh__44FF419A");

            entity.HasOne(d => d.Passenger).WithMany(p => p.Documents)
                .HasForeignKey(d => d.PassengerId)
                .HasConstraintName("FK_Documents_Passengers");

            entity.HasOne(d => d.User).WithMany(p => p.Documents)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Documents_Users");
        });

        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(e => e.FlightId).HasName("PK__Flights__E3705765106FACBE");

            entity.Property(e => e.FlightId).HasColumnName("flight_id");
            entity.Property(e => e.AircraftId).HasColumnName("aircraft_id");
            entity.Property(e => e.ArrivalAirportId).HasColumnName("arrival_airport_id");
            entity.Property(e => e.ArrivalTime)
                .HasColumnType("datetime")
                .HasColumnName("arrival_time");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.DepartureAirportId).HasColumnName("departure_airport_id");
            entity.Property(e => e.DepartureTime)
                .HasColumnType("datetime")
                .HasColumnName("departure_time");
            entity.Property(e => e.FlightNumber)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("flight_number");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("update_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Aircraft).WithMany(p => p.Flights)
                .HasForeignKey(d => d.AircraftId)
                .HasConstraintName("FK__Flights__aircraf__3C69FB99");

            entity.HasOne(d => d.ArrivalAirport).WithMany(p => p.FlightArrivalAirports)
                .HasForeignKey(d => d.ArrivalAirportId)
                .HasConstraintName("FK_Flights_Airports1");

            entity.HasOne(d => d.DepartureAirport).WithMany(p => p.FlightDepartureAirports)
                .HasForeignKey(d => d.DepartureAirportId)
                .HasConstraintName("FK_Flights_Airports");

            entity.HasOne(d => d.User).WithMany(p => p.Flights)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Flights_Users");
        });

        modelBuilder.Entity<FlightDocumentType>(entity =>
        {
            entity.HasKey(e => e.DocumentTypeId);

            entity.Property(e => e.DocumentTypeId).HasColumnName("document_type_id");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.DocumentTypeName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("document_type_name");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("update_at");
        });

        modelBuilder.Entity<GroupPermission>(entity =>
        {
            entity.HasKey(e => e.GroupId);

            entity.ToTable("Group_permission");

            entity.Property(e => e.GroupId)

                .HasColumnName("group_id");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.DocumentId).HasColumnName("document_id");
            entity.Property(e => e.GroupName)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("group_name");
            entity.Property(e => e.PermissionId).HasColumnName("permission_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("update_at");

            entity.HasOne(d => d.Document).WithMany(p => p.GroupPermissions)
                .HasForeignKey(d => d.DocumentId)
                .HasConstraintName("FK_Group_permission_Documents");

            entity.HasOne(d => d.Permission).WithMany(p => p.GroupPermissions)
                .HasForeignKey(d => d.PermissionId)
                .HasConstraintName("FK_Group_permission_Permission");

            entity.HasOne(d => d.Role).WithMany(p => p.GroupPermissions)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_Group_permission_Roles");
        });

        modelBuilder.Entity<Passenger>(entity =>
        {
            entity.HasKey(e => e.PassengerId).HasName("PK__Passenge__03764586308DB9B2");

            entity.Property(e => e.PassengerId).HasColumnName("passenger_id");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("date")
                .HasColumnName("date_of_birth");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("update_at");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.ToTable("Permission");

            entity.Property(e => e.PermissionId).HasColumnName("permission_id");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.Note)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("note");
            entity.Property(e => e.PermissionName)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("permission_name");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("update_at");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__760965CC06F59FD5");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.RoleName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("role_name");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("update_at");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__B9BE370F2CF8CC80");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .HasColumnName("address");
            entity.Property(e => e.CreateAt)
                .HasColumnType("datetime")
                .HasColumnName("create_at");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.PasswordResetToken).HasDefaultValueSql("(N'')");
            entity.Property(e => e.Phone)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.RefreshToken).HasDefaultValueSql("(N'')");
            entity.Property(e => e.RefreshTokenCreated).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");
            entity.Property(e => e.RefreshTokenExpries).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UpdateAt)
                .HasColumnType("datetime")
                .HasColumnName("update_at");
            entity.Property(e => e.UserImage)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("userImage");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.Group).WithMany(p => p.Users)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK_Users_Group_permission");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_Users_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
