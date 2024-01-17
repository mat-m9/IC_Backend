using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IC_Backend.Migrations
{
    /// <inheritdoc />
    public partial class Ver1Back : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Compras",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    usuarioCompraId = table.Column<string>(type: "text", nullable: false),
                    usuarioVentaId = table.Column<string>(type: "text", nullable: false),
                    productoId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Compras_AspNetUsers_usuarioCompraId",
                        column: x => x.usuarioCompraId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Compras_AspNetUsers_usuarioVentaId",
                        column: x => x.usuarioVentaId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Compras_Productos_productoId",
                        column: x => x.productoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Compras_productoId",
                table: "Compras",
                column: "productoId");

            migrationBuilder.CreateIndex(
                name: "IX_Compras_usuarioCompraId",
                table: "Compras",
                column: "usuarioCompraId");

            migrationBuilder.CreateIndex(
                name: "IX_Compras_usuarioVentaId",
                table: "Compras",
                column: "usuarioVentaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Compras");
        }
    }
}
