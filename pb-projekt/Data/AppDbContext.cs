using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using pb_projekt.Models;
using System.ComponentModel;

namespace pb_projekt.Data;

public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Cargo> Cargoes { get; set; }
    public DbSet<Ship> Ships { get; set; }
    public DbSet<UnloadingEquipment> UnloadingEquipments { get; set; }
    public DbSet<Hangar> Hangars { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<LandShipment> LandShipments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

}