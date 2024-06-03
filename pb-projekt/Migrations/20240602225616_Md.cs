using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace pb_projekt.Migrations
{
    /// <inheritdoc />
    public partial class Md : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hangars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    AvailableSpace = table.Column<int>(type: "int", nullable: false),
                    LoadedCrates = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hangars", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Ships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CargoCapacity = table.Column<double>(type: "double", nullable: false),
                    DockingSpace = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ships", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LandShipments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    HangarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LandShipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LandShipments_Hangars_HangarId",
                        column: x => x.HangarId,
                        principalTable: "Hangars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Cargoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SerialNumber = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    SecurityLevel = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Weight = table.Column<double>(type: "double", nullable: false),
                    CargoType = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    DestinationPort = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    ShipId = table.Column<int>(type: "int", nullable: false),
                    HangarId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cargoes_Hangars_HangarId",
                        column: x => x.HangarId,
                        principalTable: "Hangars",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cargoes_Ships_ShipId",
                        column: x => x.ShipId,
                        principalTable: "Ships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UnloadingEquipments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    IsAssignedToShip = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    SerialNumber = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    EquipmentType = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    LastInspectionDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ShipId = table.Column<int>(type: "int", nullable: true),
                    LandShipmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnloadingEquipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnloadingEquipments_LandShipments_LandShipmentId",
                        column: x => x.LandShipmentId,
                        principalTable: "LandShipments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UnloadingEquipments_Ships_ShipId",
                        column: x => x.ShipId,
                        principalTable: "Ships",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    DestinationPort = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    ArrivalDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LandShipmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_LandShipments_LandShipmentId",
                        column: x => x.LandShipmentId,
                        principalTable: "LandShipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Cargoes_HangarId",
                table: "Cargoes",
                column: "HangarId");

            migrationBuilder.CreateIndex(
                name: "IX_Cargoes_ShipId",
                table: "Cargoes",
                column: "ShipId");

            migrationBuilder.CreateIndex(
                name: "IX_LandShipments_HangarId",
                table: "LandShipments",
                column: "HangarId");

            migrationBuilder.CreateIndex(
                name: "IX_UnloadingEquipments_LandShipmentId",
                table: "UnloadingEquipments",
                column: "LandShipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_UnloadingEquipments_ShipId",
                table: "UnloadingEquipments",
                column: "ShipId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_LandShipmentId",
                table: "Vehicles",
                column: "LandShipmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cargoes");

            migrationBuilder.DropTable(
                name: "UnloadingEquipments");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Ships");

            migrationBuilder.DropTable(
                name: "LandShipments");

            migrationBuilder.DropTable(
                name: "Hangars");
        }
    }
}
