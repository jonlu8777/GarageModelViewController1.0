using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarageModelViewController.Migrations
{
    /// <inheritdoc />
    public partial class problems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ParkingSpotNr",
                table: "ParkedVehicle",
                newName: "ParkingSpot");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ParkingSpot",
                table: "ParkedVehicle",
                newName: "ParkingSpotNr");
        }
    }
}
