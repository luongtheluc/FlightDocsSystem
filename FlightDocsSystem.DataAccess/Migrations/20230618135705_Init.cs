using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightDocsSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AircraftTypes",
                columns: table => new
                {
                    aircraft_type_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    aircraft_type_name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    manufacturer = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    seating_capacity = table.Column<int>(type: "int", nullable: true),
                    create_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    update_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AircraftTypes", x => x.aircraft_type_id);
                });

            migrationBuilder.CreateTable(
                name: "Airports",
                columns: table => new
                {
                    airport_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    airport_code = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    airport_name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    city = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    country = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    create_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    update_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Airports__C795D516759AC957", x => x.airport_id);
                });

            migrationBuilder.CreateTable(
                name: "FlightDocumentTypes",
                columns: table => new
                {
                    document_type_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    document_type_name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    create_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    update_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightDocumentTypes", x => x.document_type_id);
                });

            migrationBuilder.CreateTable(
                name: "Passengers",
                columns: table => new
                {
                    passenger_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    first_name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    last_name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    date_of_birth = table.Column<DateTime>(type: "date", nullable: true),
                    gender = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    create_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    update_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Passenge__03764586308DB9B2", x => x.passenger_id);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    permission_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    permission_name = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    create_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    update_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    note = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.permission_id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role_name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    create_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    update_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Roles__760965CC06F59FD5", x => x.role_id);
                });

            migrationBuilder.CreateTable(
                name: "Aircrafts",
                columns: table => new
                {
                    aircraft_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    aircraft_number = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    aircraft_type_id = table.Column<int>(type: "int", nullable: true),
                    create_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    update_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Aircraft__04015399FA3E6786", x => x.aircraft_id);
                    table.ForeignKey(
                        name: "FK_Aircrafts_AircraftTypes",
                        column: x => x.aircraft_type_id,
                        principalTable: "AircraftTypes",
                        principalColumn: "aircraft_type_id");
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    document_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    flight_id = table.Column<int>(type: "int", nullable: true),
                    document_path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    document_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    cover_path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    document_version = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    expiration_date = table.Column<DateTime>(type: "date", nullable: true),
                    document_type_id = table.Column<int>(type: "int", nullable: true),
                    passenger_id = table.Column<int>(type: "int", nullable: true),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    create_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    update_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsConfirm = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Document__9666E8AC859AE755", x => x.document_id);
                    table.ForeignKey(
                        name: "FK_Documents_FlightDocumentTypes",
                        column: x => x.document_type_id,
                        principalTable: "FlightDocumentTypes",
                        principalColumn: "document_type_id");
                    table.ForeignKey(
                        name: "FK_Documents_Passengers",
                        column: x => x.passenger_id,
                        principalTable: "Passengers",
                        principalColumn: "passenger_id");
                });

            migrationBuilder.CreateTable(
                name: "Group_permission",
                columns: table => new
                {
                    group_id = table.Column<int>(type: "int", nullable: false),
                    document_id = table.Column<int>(type: "int", nullable: true),
                    role_id = table.Column<int>(type: "int", nullable: true),
                    permission_id = table.Column<int>(type: "int", nullable: true),
                    group_name = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    create_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    update_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group_permission", x => x.group_id);
                    table.ForeignKey(
                        name: "FK_Group_permission_Documents",
                        column: x => x.document_id,
                        principalTable: "Documents",
                        principalColumn: "document_id");
                    table.ForeignKey(
                        name: "FK_Group_permission_Permission",
                        column: x => x.permission_id,
                        principalTable: "Permission",
                        principalColumn: "permission_id");
                    table.ForeignKey(
                        name: "FK_Group_permission_Roles",
                        column: x => x.role_id,
                        principalTable: "Roles",
                        principalColumn: "role_id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    password = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    phone = table.Column<string>(type: "varchar(11)", unicode: false, maxLength: 11, nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: true),
                    userImage = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    PasswordResetToken = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(N'')"),
                    ResetTokenExpries = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VerificationToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VerifyAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(N'')"),
                    RefreshTokenCreated = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "('0001-01-01T00:00:00.0000000')"),
                    RefreshTokenExpries = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "('0001-01-01T00:00:00.0000000')"),
                    create_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    update_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    group_id = table.Column<int>(type: "int", nullable: true),
                    role_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__B9BE370F2CF8CC80", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_Users_Group_permission",
                        column: x => x.group_id,
                        principalTable: "Group_permission",
                        principalColumn: "group_id");
                    table.ForeignKey(
                        name: "FK_Users_Roles",
                        column: x => x.role_id,
                        principalTable: "Roles",
                        principalColumn: "role_id");
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    flight_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    flight_number = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    departure_airport_id = table.Column<int>(type: "int", nullable: true),
                    arrival_airport_id = table.Column<int>(type: "int", nullable: true),
                    departure_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    arrival_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    aircraft_id = table.Column<int>(type: "int", nullable: true),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    create_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    update_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Flights__E3705765106FACBE", x => x.flight_id);
                    table.ForeignKey(
                        name: "FK_Flights_Airports",
                        column: x => x.departure_airport_id,
                        principalTable: "Airports",
                        principalColumn: "airport_id");
                    table.ForeignKey(
                        name: "FK_Flights_Airports1",
                        column: x => x.arrival_airport_id,
                        principalTable: "Airports",
                        principalColumn: "airport_id");
                    table.ForeignKey(
                        name: "FK_Flights_Users",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK__Flights__aircraf__3C69FB99",
                        column: x => x.aircraft_id,
                        principalTable: "Aircrafts",
                        principalColumn: "aircraft_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aircrafts_aircraft_type_id",
                table: "Aircrafts",
                column: "aircraft_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_document_type_id",
                table: "Documents",
                column: "document_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_flight_id",
                table: "Documents",
                column: "flight_id");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_passenger_id",
                table: "Documents",
                column: "passenger_id");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_user_id",
                table: "Documents",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_aircraft_id",
                table: "Flights",
                column: "aircraft_id");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_arrival_airport_id",
                table: "Flights",
                column: "arrival_airport_id");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_departure_airport_id",
                table: "Flights",
                column: "departure_airport_id");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_user_id",
                table: "Flights",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Group_permission_document_id",
                table: "Group_permission",
                column: "document_id");

            migrationBuilder.CreateIndex(
                name: "IX_Group_permission_permission_id",
                table: "Group_permission",
                column: "permission_id");

            migrationBuilder.CreateIndex(
                name: "IX_Group_permission_role_id",
                table: "Group_permission",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_group_id",
                table: "Users",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_role_id",
                table: "Users",
                column: "role_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Users",
                table: "Documents",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK__Documents__fligh__44FF419A",
                table: "Documents",
                column: "flight_id",
                principalTable: "Flights",
                principalColumn: "flight_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aircrafts_AircraftTypes",
                table: "Aircrafts");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_FlightDocumentTypes",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Passengers",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Users",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Users",
                table: "Flights");

            migrationBuilder.DropTable(
                name: "AircraftTypes");

            migrationBuilder.DropTable(
                name: "FlightDocumentTypes");

            migrationBuilder.DropTable(
                name: "Passengers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Group_permission");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Airports");

            migrationBuilder.DropTable(
                name: "Aircrafts");
        }
    }
}
