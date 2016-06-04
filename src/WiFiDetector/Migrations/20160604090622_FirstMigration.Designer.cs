using System;
using lafe.WiFiDetector.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace lafe.WiFiDetector.Migrations
{
    [DbContext(typeof(MeasurementContext))]
    [Migration("20160604090622_FirstMigration")]
    partial class FirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20901")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WiFiDetectorWebPortal.Models.Measurement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("defaultValue", "GETUTCDATE()");

                    b.Property<string>("BSSID");

                    b.Property<int>("DeviceId");

                    b.Property<int>("Encryption");

                    b.Property<string>("SSID");

                    b.Property<double>("SignalStrength");

                    b.Property<DateTime>("Timestamp")
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("Id");

                    b.HasIndex("BSSID")
                        .HasAnnotation("SqlServer:Name", "IX_BSSID");

                    b.HasIndex("Timestamp")
                        .HasAnnotation("SqlServer:Name", "IX_Timestamp");

                    b.ToTable("Measurements");
                });
        }
    }
}
