using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuiaPlus.Migrations
{
    /// <inheritdoc />
    public partial class LatLon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "ClienteEnderecos",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "ClienteEnderecos",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "ClienteEnderecos");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "ClienteEnderecos");
        }
    }
}
