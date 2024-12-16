using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekti1.Migrations
{
    /// <inheritdoc />
    public partial class init090 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Pershkrimi",
                table: "Ushqimi",
                newName: "Origjina");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Origjina",
                table: "Ushqimi",
                newName: "Pershkrimi");
        }
    }
}
