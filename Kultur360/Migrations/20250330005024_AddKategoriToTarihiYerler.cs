using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kultur360.Migrations
{
    /// <inheritdoc />
    public partial class AddKategoriToTarihiYerler : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Kategori",
                table: "TarihiYerler",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kategori",
                table: "TarihiYerler");
        }
    }
}
