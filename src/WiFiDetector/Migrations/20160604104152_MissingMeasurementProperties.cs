using Microsoft.EntityFrameworkCore.Migrations;

namespace lafe.WiFiDetector.Migrations
{
    public partial class MissingMeasurementProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Channel",
                table: "Measurements",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Hidden",
                table: "Measurements",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Channel",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "Hidden",
                table: "Measurements");
        }
    }
}
