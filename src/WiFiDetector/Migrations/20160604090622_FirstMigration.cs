using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace lafe.WiFiDetector.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Measurements",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValue: "newsequentialid()"),
                    BSSID = table.Column<string>(nullable: true),
                    DeviceId = table.Column<int>(nullable: false),
                    Encryption = table.Column<int>(nullable: false),
                    SSID = table.Column<string>(nullable: true),
                    SignalStrength = table.Column<double>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false, defaultValue: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measurements", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BSSID",
                table: "Measurements",
                column: "BSSID");

            migrationBuilder.CreateIndex(
                name: "IX_Timestamp",
                table: "Measurements",
                column: "Timestamp");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Measurements");
        }
    }
}
