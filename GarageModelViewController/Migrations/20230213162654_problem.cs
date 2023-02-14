using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarageModelViewController.Migrations
{
    /// <inheritdoc />
    public partial class problem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParkingSpot",
                table: "ParkedVehicle");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParkingSpot",
                table: "ParkedVehicle",
                type: "int",
                nullable: true);
        }
    }
}
