using Microsoft.EntityFrameworkCore;

namespace lafe.WiFiDetector.Models
{
    public class MeasurementContext : DbContext
    {
        public MeasurementContext(DbContextOptions<MeasurementContext> options)
            : base(options)
        { }

        public DbSet<Measurement> Measurements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Measurement>().HasIndex(m => m.Timestamp).ForSqlServerHasName("IX_Timestamp");
            modelBuilder.Entity<Measurement>().HasIndex(m => m.BSSID).ForSqlServerHasName("IX_BSSID");
        }
    }
}