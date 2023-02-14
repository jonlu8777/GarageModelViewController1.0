using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarageModelViewController.Migrations
{
    /// <inheritdoc />
    public partial class parkingspots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Ankomsttid",
                table: "ParkedVehicle",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "ParkingSpot",
                table: "ParkedVehicle",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParkingSpot",
                table: "ParkedVehicle");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Ankomsttid",
                table: "ParkedVehicle",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
