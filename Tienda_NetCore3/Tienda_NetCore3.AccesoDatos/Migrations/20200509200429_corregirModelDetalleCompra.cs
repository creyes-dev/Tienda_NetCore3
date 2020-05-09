using Microsoft.EntityFrameworkCore.Migrations;

namespace Tienda_NetCore3.AccesoDatos.Migrations
{
    public partial class corregirModelDetalleCompra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalleCompra_Servicio_IdEncabezadoCompra",
                table: "DetalleCompra");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleCompra_IdServicio",
                table: "DetalleCompra",
                column: "IdServicio");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleCompra_Servicio_IdServicio",
                table: "DetalleCompra",
                column: "IdServicio",
                principalTable: "Servicio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalleCompra_Servicio_IdServicio",
                table: "DetalleCompra");

            migrationBuilder.DropIndex(
                name: "IX_DetalleCompra_IdServicio",
                table: "DetalleCompra");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleCompra_Servicio_IdEncabezadoCompra",
                table: "DetalleCompra",
                column: "IdEncabezadoCompra",
                principalTable: "Servicio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
