using Diplom.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
            builder.Entity<RouteEn>().HasData(
                new RouteEn
                {
                    Id = Guid.NewGuid(),
                    Price = 120000,
                    StartPointId = Guid.Parse("8dd63283-1bbd-4ffd-9a15-eb806c41614f"),
                    EndPointId = Guid.Parse("92abfca3-7e4a-42d7-bc24-e3079575057a")
                });

        }
    }
}
