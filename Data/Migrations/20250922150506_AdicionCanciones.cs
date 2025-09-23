using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MusicaNobaMVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionCanciones : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.InsertData(
                table: "Canciones",
                columns: new[] { "IdCancion", "AlbumId", "Artista", "GeneroId", "Nombre" },
                values: new object[,]
                {
                    { 2, 2, "Motley Crue", 1, "Knockin' On Heaven's Door" },
                    { 3, 2, "Guns N' Roses", 1, "Welcome To The Jungle" },
                    { 4, 2, "Guns N' Roses", 1, "Sweet Child O' Mine" },
                    { 5, 3, "Whiplash Soundtrack", 3, "Whiplash" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Canciones",
                keyColumn: "IdCancion",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Canciones",
                keyColumn: "IdCancion",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Canciones",
                keyColumn: "IdCancion",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Canciones",
                keyColumn: "IdCancion",
                keyValue: 5);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duracion",
                table: "Canciones",
                type: "time",
                nullable: true);
        }
    }
}
