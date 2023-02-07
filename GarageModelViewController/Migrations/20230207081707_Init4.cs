using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarageModelViewController.Migrations
{
    /// <inheritdoc />
    public partial class Init4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ParkedVehicle",
                table: "ParkedVehicle");

            migrationBuilder.AlterColumn<string>(
                name: "RegistrationNumber",
                table: "ParkedVehicle",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ParkedVehicle",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParkedVehicle",
                table: "ParkedVehicle",
                column: "RegistrationNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ParkedVehicle",
                table: "ParkedVehicle");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ParkedVehicle",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "RegistrationNumber",
                table: "ParkedVehicle",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(6)",
                oldMaxLength: 6);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParkedVehicle",
                table: "ParkedVehicle",
                column: "Id");
        }
    }
}
