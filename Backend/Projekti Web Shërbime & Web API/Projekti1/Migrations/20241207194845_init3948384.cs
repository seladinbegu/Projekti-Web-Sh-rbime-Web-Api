using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekti1.Migrations
{
    /// <inheritdoc />
    public partial class init3948384 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Ushqimi",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Ushqimi");
        }
    }
}
