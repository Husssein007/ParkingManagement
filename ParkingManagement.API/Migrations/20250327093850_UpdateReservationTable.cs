using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingManagement.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReservationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_ParkingSpots_SpotId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_SpotId",
                table: "Reservations");

            migrationBuilder.AddColumn<int>(
                name: "ParkingSpotId",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ParkingSpotId",
                table: "Reservations",
                column: "ParkingSpotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_ParkingSpots_ParkingSpotId",
                table: "Reservations",
                column: "ParkingSpotId",
                principalTable: "ParkingSpots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_ParkingSpots_ParkingSpotId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ParkingSpotId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ParkingSpotId",
                table: "Reservations");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_SpotId",
                table: "Reservations",
                column: "SpotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_ParkingSpots_SpotId",
                table: "Reservations",
                column: "SpotId",
                principalTable: "ParkingSpots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
