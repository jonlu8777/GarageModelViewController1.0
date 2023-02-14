using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarageModelViewController.Migrations
{
    /// <inheritdoc />
    public partial class parkNr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParkingSpotNr",
                table: "ParkedVehicle",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParkingSpotNr",
                table: "ParkedVehicle");
        }
    }
}
