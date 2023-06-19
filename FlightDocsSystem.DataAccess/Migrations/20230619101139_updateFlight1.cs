using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightDocsSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateFlight1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "flight_number",
                table: "Flights",
                type: "varchar(150)",
                unicode: false,
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldUnicode: false,
                oldMaxLength: 10,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "flight_number",
                table: "Flights",
                type: "varchar(10)",
                unicode: false,
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldUnicode: false,
                oldMaxLength: 150,
                oldNullable: true);
        }
    }
}
