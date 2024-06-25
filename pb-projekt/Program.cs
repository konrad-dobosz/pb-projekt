using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
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

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(15);

    options.LoginPath = "/auth/login";
    options.AccessDeniedPath = "/auth/login";
    options.SlidingExpiration = true;
});

var app = builder.Build();

SeedDatabase(app);

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(e => e.MapControllers().RequireAuthorization());

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
        context.Hangars.RemoveRange(context.Hangars);
        context.LandShipments.RemoveRange(context.LandShipments);
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
            context.SaveChanges();

            foreach (var ship in ships)
            {
                // Create cargoes for ships
                var shipCargoes = new List<Cargo>
                {
                    new Cargo { SerialNumber = $"Ship-SN1-{ship.Id}", SecurityLevel = "High", Weight = 100, CargoType = "General", DestinationPort = "Port A", ShipId = ship.Id },
                    new Cargo { SerialNumber = $"Ship-SN2-{ship.Id}", SecurityLevel = "High", Weight = 110, CargoType = "General", DestinationPort = "Port B", ShipId = ship.Id },
                    new Cargo { SerialNumber = $"Ship-SN3-{ship.Id}", SecurityLevel = "High", Weight = 120, CargoType = "General", DestinationPort = "Port C", ShipId = ship.Id },
                    new Cargo { SerialNumber = $"Ship-SN4-{ship.Id}", SecurityLevel = "High", Weight = 130, CargoType = "General", DestinationPort = "Port D", ShipId = ship.Id },
                    new Cargo { SerialNumber = $"Ship-SN5-{ship.Id}", SecurityLevel = "High", Weight = 140, CargoType = "General", DestinationPort = "Port E", ShipId = ship.Id }
                };

                // Add cargoes to ship
                context.Cargoes.AddRange(shipCargoes);
                context.SaveChanges();

                // Create hangar and associated cargoes
                var hangarCargoes = new List<Cargo>
                {
                    new Cargo { SerialNumber = $"Hangar-SN1-{ship.Id}", SecurityLevel = "High", Weight = 200, CargoType = "General", DestinationPort = "Port F", ShipId = ship.Id },
                    new Cargo { SerialNumber = $"Hangar-SN2-{ship.Id}", SecurityLevel = "High", Weight = 210, CargoType = "General", DestinationPort = "Port G", ShipId = ship.Id },
                    new Cargo { SerialNumber = $"Hangar-SN3-{ship.Id}", SecurityLevel = "High", Weight = 220, CargoType = "General", DestinationPort = "Port H", ShipId = ship.Id },
                    new Cargo { SerialNumber = $"Hangar-SN4-{ship.Id}", SecurityLevel = "High", Weight = 230, CargoType = "General", DestinationPort = "Port I", ShipId = ship.Id },
                    new Cargo { SerialNumber = $"Hangar-SN5-{ship.Id}", SecurityLevel = "High", Weight = 240, CargoType = "General", DestinationPort = "Port J", ShipId = ship.Id }
                };

                var hangar = new Hangar { AvailableSpace = 1000, Cargoes = hangarCargoes };
                context.Hangars.Add(hangar);
                context.SaveChanges();

                // Create land shipment and associated cargoes
                var landShipmentCargoes = new List<Cargo>
                {
                    new Cargo { SerialNumber = $"LandShipment-SN1-{ship.Id}", SecurityLevel = "High", Weight = 300, CargoType = "General", DestinationPort = "Port K", ShipId = ship.Id },
                    new Cargo { SerialNumber = $"LandShipment-SN2-{ship.Id}", SecurityLevel = "High", Weight = 310, CargoType = "General", DestinationPort = "Port L", ShipId = ship.Id },
                    new Cargo { SerialNumber = $"LandShipment-SN3-{ship.Id}", SecurityLevel = "High", Weight = 320, CargoType = "General", DestinationPort = "Port M", ShipId = ship.Id },
                    new Cargo { SerialNumber = $"LandShipment-SN4-{ship.Id}", SecurityLevel = "High", Weight = 330, CargoType = "General", DestinationPort = "Port N", ShipId = ship.Id },
                    new Cargo { SerialNumber = $"LandShipment-SN5-{ship.Id}", SecurityLevel = "High", Weight = 340, CargoType = "General", DestinationPort = "Port O", ShipId = ship.Id }
                };

                var landShipment = new LandShipment { HangarId = hangar.Id, Cargoes = landShipmentCargoes };
                context.LandShipments.Add(landShipment);
                context.SaveChanges();
            }

            // Add unloading equipments for the second ship
            var secondShip = context.Ships.FirstOrDefault(s => s.DockingSpace == "Dock 2");
            if (secondShip != null)
            {
                var unloadingEquipments = new List<UnloadingEquipment>
                {
                    new UnloadingEquipment { SerialNumber = "UE1", EquipmentType = "Crane", LastInspectionDate = DateTime.Now, ShipId = secondShip.Id, IsAssignedToShip = true },
                    new UnloadingEquipment { SerialNumber = "UE2", EquipmentType = "Forklift", LastInspectionDate = DateTime.Now, ShipId = secondShip.Id, IsAssignedToShip = true }
                };

                context.UnloadingEquipments.AddRange(unloadingEquipments);
                context.SaveChanges();
            }

            // Add unassigned unloading equipments
            var unassignedUnloadingEquipments = new List<UnloadingEquipment>
            {
                new UnloadingEquipment { SerialNumber = "UE3", EquipmentType = "Conveyor Belt", LastInspectionDate = DateTime.Now, ShipId = null, IsAssignedToShip = false },
                new UnloadingEquipment { SerialNumber = "UE4", EquipmentType = "Loading Arm", LastInspectionDate = DateTime.Now, ShipId = null, IsAssignedToShip = false }
            };

            context.UnloadingEquipments.AddRange(unassignedUnloadingEquipments);
            context.SaveChanges();
        }
    }
}
