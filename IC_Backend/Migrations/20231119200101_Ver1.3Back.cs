using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IC_Backend.Migrations
{
    /// <inheritdoc />
    public partial class Ver13Back : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "cantidad",
                table: "Compras",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "total",
                table: "Compras",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cantidad",
                table: "Compras");

            migrationBuilder.DropColumn(
                name: "total",
                table: "Compras");
        }
    }
}
