using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pb_projekt.Migrations
{
    /// <inheritdoc />
    public partial class Lshien : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LandShipmentId",
                table: "Cargoes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cargoes_LandShipmentId",
                table: "Cargoes",
                column: "LandShipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cargoes_LandShipments_LandShipmentId",
                table: "Cargoes",
                column: "LandShipmentId",
                principalTable: "LandShipments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cargoes_LandShipments_LandShipmentId",
                table: "Cargoes");

            migrationBuilder.DropIndex(
                name: "IX_Cargoes_LandShipmentId",
                table: "Cargoes");

            migrationBuilder.DropColumn(
                name: "LandShipmentId",
                table: "Cargoes");
        }
    }
}
