using DisasterAlertSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DisasterAlertSystemAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Region> regions { get; set; }
        public DbSet<AlertSetting> alertSettings { get; set; }
        public DbSet<DisasterRisk> disasterRisks { get; set; }
        public DbSet<AlertData> alertDatas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Region>().HasKey(r => r.RegionId);
            modelBuilder.Entity<AlertSetting>().HasKey(a => new { a.RegionId, a.DisasterTypes });
            modelBuilder.Entity<DisasterRisk>().HasKey(d => new { d.RegionId, d.DisasterType });
        }
    }
}
