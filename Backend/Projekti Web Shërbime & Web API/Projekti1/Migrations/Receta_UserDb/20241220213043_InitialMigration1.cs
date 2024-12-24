using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekti1.Migrations.Receta_UserDb
{
    /// <inheritdoc />
    public partial class InitialMigration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecetaUser_Receta_RecetaId",
                table: "RecetaUser");

            migrationBuilder.DropForeignKey(
                name: "FK_RecetaUser_User_UserId",
                table: "RecetaUser");

            migrationBuilder.DropTable(
                name: "DietaUshqimi");

            migrationBuilder.DropTable(
                name: "RecetaUshqimi");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Dieta");

            migrationBuilder.DropTable(
                name: "Receta");

            migrationBuilder.DropTable(
                name: "Ushqimi");

            migrationBuilder.DropIndex(
                name: "IX_RecetaUser_RecetaId",
                table: "RecetaUser");

            migrationBuilder.DropIndex(
                name: "IX_RecetaUser_UserId",
                table: "RecetaUser");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "RecetaUser",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "RecetaUser",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Dieta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataKrijimit = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Emri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lloji = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pershkrimi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dieta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Receta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataKrijimit = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Emri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Udhezimet = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ushqimi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataKrijimit = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Emri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fibrat = table.Column<double>(type: "float", nullable: false),
                    Hekur = table.Column<double>(type: "float", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kalcium = table.Column<double>(type: "float", nullable: false),
                    Kalori = table.Column<double>(type: "float", nullable: false),
                    Karbohidrate = table.Column<double>(type: "float", nullable: false),
                    Kategoria = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Origjina = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Proteina = table.Column<double>(type: "float", nullable: false),
                    Vegan = table.Column<bool>(type: "bit", nullable: false),
                    VitaminA = table.Column<double>(type: "float", nullable: false),
                    VitaminC = table.Column<double>(type: "float", nullable: false),
                    Yndyrna = table.Column<double>(type: "float", nullable: false),
                    kaBulmet = table.Column<bool>(type: "bit", nullable: false),
                    kaGluten = table.Column<bool>(type: "bit", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "RecetaUshqimi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecetaId = table.Column<int>(type: "int", nullable: false),
                    UshqimiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecetaUshqimi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecetaUshqimi_Receta_RecetaId",
                        column: x => x.RecetaId,
                        principalTable: "Receta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecetaUshqimi_Ushqimi_UshqimiId",
                        column: x => x.UshqimiId,
                        principalTable: "Ushqimi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecetaUser_RecetaId",
                table: "RecetaUser",
                column: "RecetaId");

            migrationBuilder.CreateIndex(
                name: "IX_RecetaUser_UserId",
                table: "RecetaUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DietaUshqimi_DietaId",
                table: "DietaUshqimi",
                column: "DietaId");

            migrationBuilder.CreateIndex(
                name: "IX_DietaUshqimi_UshqimiId",
                table: "DietaUshqimi",
                column: "UshqimiId");

            migrationBuilder.CreateIndex(
                name: "IX_RecetaUshqimi_RecetaId",
                table: "RecetaUshqimi",
                column: "RecetaId");

            migrationBuilder.CreateIndex(
                name: "IX_RecetaUshqimi_UshqimiId",
                table: "RecetaUshqimi",
                column: "UshqimiId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecetaUser_Receta_RecetaId",
                table: "RecetaUser",
                column: "RecetaId",
                principalTable: "Receta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RecetaUser_User_UserId",
                table: "RecetaUser",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
