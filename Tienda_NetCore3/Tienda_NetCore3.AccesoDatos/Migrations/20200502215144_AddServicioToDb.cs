using Microsoft.EntityFrameworkCore.Migrations;

namespace Tienda_NetCore3.AccesoDatos.Migrations
{
    public partial class AddServicioToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Servicio",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: false),
                    Precio = table.Column<double>(nullable: false),
                    Descripcion = table.Column<string>(nullable: true),
                    UrlImagen = table.Column<string>(nullable: true),
                    IdCategoria = table.Column<int>(nullable: false),
                    IdFrecuencia = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Servicio_Categoria_IdCategoria",
                        column: x => x.IdCategoria,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Servicio_Frecuencia_IdFrecuencia",
                        column: x => x.IdFrecuencia,
                        principalTable: "Frecuencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Servicio_IdCategoria",
                table: "Servicio",
                column: "IdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Servicio_IdFrecuencia",
                table: "Servicio",
                column: "IdFrecuencia");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Servicio");
        }
    }
}
