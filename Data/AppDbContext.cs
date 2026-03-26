using DisasterAlertSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DisasterAlertSystemAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Regions> regions { get; set; }
        public DbSet<AlertSettings> alertSettings { get; set; }
    }
}
