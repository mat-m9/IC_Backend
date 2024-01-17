using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IC_Backend.Migrations
{
    /// <inheritdoc />
    public partial class Ver13Backaw : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Compras",
                type: "text",
                nullable: false,
                defaultValue: "uuid_generate_v4()",
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Compras",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValue: "uuid_generate_v4()");
        }
    }
}
