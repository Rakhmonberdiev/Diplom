using Diplom.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Diplom.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions opt) : base(opt)
        {
            
        }

        public DbSet<DistrictsEn> Districts { get; set; }
        public DbSet<RouteEn> Routes { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<RouteEn>()
                .HasOne(r => r.StartPoint)
                .WithMany()
                .HasForeignKey(r => r.StartPointId);

            builder.Entity<RouteEn>()
                .HasOne(r => r.EndPoint)
                .WithMany()
                .HasForeignKey(r => r.EndPointId);
            builder.Entity<Ticket>()
                .HasOne(r=>r.Route)
                .WithMany()
                .HasForeignKey(r=>r.RouteId);
            builder.Entity<Ticket>()
                .HasOne(r=>r.Schedule)
                .WithMany()
                .HasForeignKey(r=>r.ScheduleId);
            builder.Entity<Ticket>()
                .HasOne(r=>r.AppUser)
                .WithMany()
                .HasForeignKey(r=>r.UserId);
        }
    }
}
