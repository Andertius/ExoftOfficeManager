using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace ExoftOfficeManager.DataAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        //public DbSet<Booking> Bookings { get; set; }

        //public DbSet<Meeting> Meetings { get; set; }

        //public DbSet<WorkPlace> WorkPlaces { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> opts)
            : base(opts)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(a => a.Id);  
        }
    }
}
