using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightDocsSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updateDocument1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "document_type",
                table: "Documents",
                newName: "document_path");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "document_path",
                table: "Documents",
                newName: "document_type");
        }
    }
}
