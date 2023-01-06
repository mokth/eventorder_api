using EventDemoAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace EventDemoAPI.DBContext
{
    public class DbEventContext : DbContext
    {
        public DbContextOptions<DbEventContext> sqloptions;
        public DbEventContext(DbContextOptions<DbEventContext> options) : base(options)
        {
            sqloptions = options;
        }

        public virtual DbSet<EventDemo> EventDemos { get; set; }
        public virtual DbSet<EventBooking> EventBookings { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<EventView> EventViews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventView>
                (entity =>
                {
                    entity.HasKey(e => e.eventId);
                    entity.ToTable("vEventList");
                });

        }
    }
}
