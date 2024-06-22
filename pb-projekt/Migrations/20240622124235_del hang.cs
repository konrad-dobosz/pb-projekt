using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pb_projekt.Migrations
{
    /// <inheritdoc />
    public partial class delhang : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LoadedCrates",
                table: "Hangars");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LoadedCrates",
                table: "Hangars",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
