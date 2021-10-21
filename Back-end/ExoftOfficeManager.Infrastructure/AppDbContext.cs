using ExoftOfficeManager.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace ExoftOfficeManager.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Meeting> Meetings { get; set; }

        public DbSet<WorkPlace> WorkPlaces { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> opts)
            : base(opts)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Meeting>()
                .HasOne(x => x.Owner)
                .WithMany(x => x.OwnerMeetings)
                .HasForeignKey(x => x.OwnerId);


            modelBuilder.Entity<RequiredUserMeeting>()
                .HasOne(x => x.RequiredUser)
                .WithMany(x => x.RequiredUserMeetings)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<RequiredUserMeeting>()
                .HasOne(x => x.Meeting)
                .WithMany(x => x.RequiredUserMeetings)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<NotRequiredUserMeeting>()
                .HasOne(x => x.NotRequiredUser)
                .WithMany(x => x.NotRequiredUserMeetings)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<NotRequiredUserMeeting>()
                .HasOne(x => x.Meeting)
                .WithMany(x => x.NotRequiredUserMeetings)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
