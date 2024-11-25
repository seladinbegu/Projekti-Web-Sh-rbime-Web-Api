using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekti1.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ushqimi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kalori = table.Column<double>(type: "float", nullable: false),
                    Proteina = table.Column<double>(type: "float", nullable: false),
                    Karbohidrate = table.Column<double>(type: "float", nullable: false),
                    Yndyrna = table.Column<double>(type: "float", nullable: false),
                    Fibrat = table.Column<double>(type: "float", nullable: false),
                    VitaminC = table.Column<double>(type: "float", nullable: false),
                    VitaminA = table.Column<double>(type: "float", nullable: false),
                    Kalcium = table.Column<double>(type: "float", nullable: false),
                    Hekur = table.Column<double>(type: "float", nullable: false),
                    Vegan = table.Column<bool>(type: "bit", nullable: false),
                    kaGluten = table.Column<bool>(type: "bit", nullable: false),
                    kaBulmet = table.Column<bool>(type: "bit", nullable: false),
                    Kategoria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pershkrimi = table.Column<long>(type: "bigint", nullable: false),
                    DataKrijimit = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ushqimi", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ushqimi");
        }
    }
}
