using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.AccesData.Migrations
{
    /// <inheritdoc />
    public partial class obl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cabañas",
                columns: table => new
                {
                    numeroHabitacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreTipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    habilitada = table.Column<bool>(type: "bit", nullable: false),
                    nombre_Valor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    poseeJacuzzi = table.Column<bool>(type: "bit", nullable: false),
                    capacidad = table.Column<int>(type: "int", nullable: false),
                    Fotos_Valor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cabañas", x => x.numeroHabitacion);
                });

            migrationBuilder.CreateTable(
                name: "Configuraciones",
                columns: table => new
                {
                    Atributo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LimiteSuperior = table.Column<int>(type: "int", nullable: true),
                    LimiteInferior = table.Column<int>(type: "int", nullable: true),
                    LimiteSuperiorDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LimiteInferiorDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Configuraciones", x => x.Atributo);
                });

            migrationBuilder.CreateTable(
                name: "mantenimientos",
                columns: table => new
                {
                    mantenimientoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Habitacion = table.Column<int>(type: "int", nullable: false),
                    FechaMantenimiento_Valor = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Costo = table.Column<double>(type: "float", nullable: false),
                    NombreTrabajador = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mantenimientos", x => x.mantenimientoId);
                });

            migrationBuilder.CreateTable(
                name: "tipos",
                columns: table => new
                {
                    nombre = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    costoXHuesped = table.Column<decimal>(type: "decimal(18,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipos", x => x.nombre);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Contrasena_Valor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreCompleto = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.Email);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cabañas");

            migrationBuilder.DropTable(
                name: "Configuraciones");

            migrationBuilder.DropTable(
                name: "mantenimientos");

            migrationBuilder.DropTable(
                name: "tipos");

            migrationBuilder.DropTable(
                name: "usuarios");
        }
    }
}
