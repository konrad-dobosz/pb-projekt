using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using pb_projekt.Data;
using pb_projekt.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySQL(connectionString));

builder.Services.AddDefaultIdentity<User>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 1;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
})
    .AddEntityFrameworkStores<AppDbContext>();

var app = builder.Build();

SeedDatabase(app);

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

void SeedDatabase(IHost app)
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<AppDbContext>();

        // Clear existing data
        context.Ships.RemoveRange(context.Ships);
        context.Cargoes.RemoveRange(context.Cargoes);
        context.UnloadingEquipments.RemoveRange(context.UnloadingEquipments);
        context.SaveChanges();

        // Add new data
        context.Database.EnsureCreated();

        if (!context.Ships.Any())
        {
            var ships = new List<Ship>
            {
                new Ship { CargoCapacity = 1000, DockingSpace = "Dock 1" },
                new Ship { CargoCapacity = 1500, DockingSpace = "Dock 2" },
                new Ship { CargoCapacity = 2000, DockingSpace = "Dock 3" },
                new Ship { CargoCapacity = 2500, DockingSpace = "Dock 4" },
                new Ship { CargoCapacity = 3000, DockingSpace = "Dock 5" }
            };

            context.Ships.AddRange(ships);
            context.SaveChanges(); // Save to generate the Ship IDs

            foreach (var ship in ships)
            {
                var cargos = new List<Cargo>
                {
                    new Cargo { SerialNumber = $"SN1-{ship.Id}", SecurityLevel = "High", Weight = 100, CargoType = "General", DestinationPort = "Port A", ShipId = ship.Id },
                    new Cargo { SerialNumber = $"SN2-{ship.Id}", SecurityLevel = "High", Weight = 110, CargoType = "General", DestinationPort = "Port B", ShipId = ship.Id },
                    new Cargo { SerialNumber = $"SN3-{ship.Id}", SecurityLevel = "High", Weight = 120, CargoType = "General", DestinationPort = "Port C", ShipId = ship.Id },
                    new Cargo { SerialNumber = $"SN4-{ship.Id}", SecurityLevel = "High", Weight = 130, CargoType = "General", DestinationPort = "Port D", ShipId = ship.Id },
                    new Cargo { SerialNumber = $"SN5-{ship.Id}", SecurityLevel = "High", Weight = 140, CargoType = "General", DestinationPort = "Port E", ShipId = ship.Id }
                };

                context.Cargoes.AddRange(cargos);
            }
            context.SaveChanges(); // Save changes to add cargoes to database
        }

        // Add unloading equipment to the second ship
        var secondShip = context.Ships.FirstOrDefault(s => s.DockingSpace == "Dock 2");
        if (secondShip != null)
        {
            var unloadingEquipments = new List<UnloadingEquipment>
            {
                new UnloadingEquipment { SerialNumber = "UE1", EquipmentType = "Crane", LastInspectionDate = DateTime.Now, ShipId = secondShip.Id, IsAssignedToShip = true },
                new UnloadingEquipment { SerialNumber = "UE2", EquipmentType = "Forklift", LastInspectionDate = DateTime.Now, ShipId = secondShip.Id, IsAssignedToShip = true }
            };

            context.UnloadingEquipments.AddRange(unloadingEquipments);
            context.SaveChanges(); // Save changes to add unloading equipment to database
        }
    }
}
