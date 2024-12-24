using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekti1.Migrations.DietaDb
{
    /// <inheritdoc />
    public partial class Initial : Migration
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
                    Kategoria = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Origjina = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataKrijimit = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ushqimi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DietaUshqimi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DietaId = table.Column<int>(type: "int", nullable: false),
                    UshqimiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietaUshqimi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DietaUshqimi_Dieta_DietaId",
                        column: x => x.DietaId,
                        principalTable: "Dieta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DietaUshqimi_Ushqimi_UshqimiId",
                        column: x => x.UshqimiId,
                        principalTable: "Ushqimi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DietaUshqimi_DietaId",
                table: "DietaUshqimi",
                column: "DietaId");

            migrationBuilder.CreateIndex(
                name: "IX_DietaUshqimi_UshqimiId",
                table: "DietaUshqimi",
                column: "UshqimiId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DietaUshqimi");

            migrationBuilder.DropTable(
                name: "Ushqimi");
        }
    }
}
