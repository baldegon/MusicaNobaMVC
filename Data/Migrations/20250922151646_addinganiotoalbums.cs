using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicaNobaMVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class addinganiotoalbums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Albums",
                keyColumn: "IdAlbum",
                keyValue: 1,
                column: "Anio",
                value: 1984);

            migrationBuilder.UpdateData(
                table: "Albums",
                keyColumn: "IdAlbum",
                keyValue: 2,
                column: "Anio",
                value: 1986);

            migrationBuilder.UpdateData(
                table: "Albums",
                keyColumn: "IdAlbum",
                keyValue: 3,
                column: "Anio",
                value: 2014);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Albums",
                keyColumn: "IdAlbum",
                keyValue: 1,
                column: "Anio",
                value: null);

            migrationBuilder.UpdateData(
                table: "Albums",
                keyColumn: "IdAlbum",
                keyValue: 2,
                column: "Anio",
                value: null);

            migrationBuilder.UpdateData(
                table: "Albums",
                keyColumn: "IdAlbum",
                keyValue: 3,
                column: "Anio",
                value: null);
        }
    }
}
