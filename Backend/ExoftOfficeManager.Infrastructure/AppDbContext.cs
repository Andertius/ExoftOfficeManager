using System.Threading;
using System.Threading.Tasks;

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

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {


            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
                .HasOne(x => x.User)
                .WithMany(x => x.Bookings)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<Booking>()
                .HasOne(x => x.WorkPlace)
                .WithMany(x => x.Bookings)
                .HasForeignKey(x => x.WorkPlaceId);


            modelBuilder.Entity<Meeting>()
                .HasOne(x => x.Owner)
                .WithMany(x => x.OwnerMeetings);


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
