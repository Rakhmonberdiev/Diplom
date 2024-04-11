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
        }
    }
}
