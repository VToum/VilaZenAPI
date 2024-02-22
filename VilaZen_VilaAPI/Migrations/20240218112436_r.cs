using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VilaZenVilaAPI.Migrations
{
    /// <inheritdoc />
    public partial class r : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Avaliar",
                table: "Villas",
                newName: "Preco");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Preco",
                table: "Villas",
                newName: "Avaliar");
        }
    }
}
