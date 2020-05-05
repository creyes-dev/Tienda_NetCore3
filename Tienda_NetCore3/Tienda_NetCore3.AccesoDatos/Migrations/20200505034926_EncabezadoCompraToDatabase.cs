using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tienda_NetCore3.AccesoDatos.Migrations
{
    public partial class EncabezadoCompraToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EncabezadoCompra",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: false),
                    Telefono = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Direccion = table.Column<string>(nullable: false),
                    CodigoPostal = table.Column<string>(nullable: false),
                    FechaCompra = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    Comentarios = table.Column<string>(nullable: true),
                    ConteoServicios = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EncabezadoCompra", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DetalleCompra",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEncabezadoCompra = table.Column<int>(nullable: false),
                    IdServicio = table.Column<int>(nullable: false),
                    NombreServicio = table.Column<string>(nullable: false),
                    Precio = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleCompra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetalleCompra_EncabezadoCompra_IdEncabezadoCompra",
                        column: x => x.IdEncabezadoCompra,
                        principalTable: "EncabezadoCompra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleCompra_Servicio_IdEncabezadoCompra",
                        column: x => x.IdEncabezadoCompra,
                        principalTable: "Servicio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetalleCompra_IdEncabezadoCompra",
                table: "DetalleCompra",
                column: "IdEncabezadoCompra");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalleCompra");

            migrationBuilder.DropTable(
                name: "EncabezadoCompra");
        }
    }
}
