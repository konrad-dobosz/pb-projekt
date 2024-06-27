using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using pb_projekt.Models;

namespace pb_projekt.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Cargo> Cargoes { get; set; }
        public DbSet<Ship> Ships { get; set; }
        public DbSet<UnloadingEquipment> UnloadingEquipments { get; set; }
        public DbSet<Hangar> Hangars { get; set; }
        public DbSet<LandShipment> LandShipments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ship>()
                .HasMany(s => s.Cargoes)
                .WithOne(c => c.Ship)
                .HasForeignKey(c => c.ShipId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Hangar>()
                .HasMany(h => h.Cargoes)
                .WithOne(c => c.Hangar)
                .HasForeignKey(c => c.HangarId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<UnloadingEquipment>()
                .HasOne(ue => ue.Ship)
                .WithMany(s => s.UnloadingEquipments)
                .HasForeignKey(ue => ue.ShipId);
        }
    }
}
