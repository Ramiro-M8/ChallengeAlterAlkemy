using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IconosGeograficos.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "continentes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    urlImagen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    denominacion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_continentes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "iconosGeograficos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    urlImagen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    denominacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    altura = table.Column<double>(type: "float", nullable: false),
                    historia = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_iconosGeograficos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ciudades",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    urlImagen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    denominacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cantidadHabitantes = table.Column<int>(type: "int", nullable: false),
                    superficieTotal = table.Column<double>(type: "float", nullable: false),
                    continenteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ciudades", x => x.id);
                    table.ForeignKey(
                        name: "FK_ciudades_continentes_continenteId",
                        column: x => x.continenteId,
                        principalTable: "continentes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CiudadesIconosGeograficos",
                columns: table => new
                {
                    ciudadesid = table.Column<int>(type: "int", nullable: false),
                    iconosGeograficosid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CiudadesIconosGeograficos", x => new { x.ciudadesid, x.iconosGeograficosid });
                    table.ForeignKey(
                        name: "FK_CiudadesIconosGeograficos_ciudades_ciudadesid",
                        column: x => x.ciudadesid,
                        principalTable: "ciudades",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CiudadesIconosGeograficos_iconosGeograficos_iconosGeograficosid",
                        column: x => x.iconosGeograficosid,
                        principalTable: "iconosGeograficos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ciudades_continenteId",
                table: "ciudades",
                column: "continenteId");

            migrationBuilder.CreateIndex(
                name: "IX_CiudadesIconosGeograficos_iconosGeograficosid",
                table: "CiudadesIconosGeograficos",
                column: "iconosGeograficosid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CiudadesIconosGeograficos");

            migrationBuilder.DropTable(
                name: "ciudades");

            migrationBuilder.DropTable(
                name: "iconosGeograficos");

            migrationBuilder.DropTable(
                name: "continentes");
        }
    }
}
