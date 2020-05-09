using Microsoft.EntityFrameworkCore.Migrations;

namespace Tienda_NetCore3.AccesoDatos.Migrations
{
    public partial class EstadoEncabezadoCompraToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "EncabezadoCompra");

            migrationBuilder.AddColumn<string>(
                name: "Ciudad",
                table: "EncabezadoCompra",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "EncabezadoCompra",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ciudad",
                table: "EncabezadoCompra");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "EncabezadoCompra");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "EncabezadoCompra",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
