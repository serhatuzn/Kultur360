using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kultur360.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTarihiYerModeli : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Isım",
                table: "TarihiYerler",
                newName: "Isim");

            migrationBuilder.RenameColumn(
                name: "Acıklama",
                table: "TarihiYerler",
                newName: "Aciklama");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Isim",
                table: "TarihiYerler",
                newName: "Isım");

            migrationBuilder.RenameColumn(
                name: "Aciklama",
                table: "TarihiYerler",
                newName: "Acıklama");
        }
    }
}
