using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightDocsSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateDocument2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "document_number",
                table: "Documents",
                newName: "document_version");

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirm",
                table: "Documents",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsConfirm",
                table: "Documents");

            migrationBuilder.RenameColumn(
                name: "document_version",
                table: "Documents",
                newName: "document_number");
        }
    }
}
