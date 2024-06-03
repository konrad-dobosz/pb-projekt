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

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<AppDbContext>();

    // Dodaj przyk³adowe statki do bazy danych
    if (!dbContext.Ships.Any())
    {
        dbContext.Ships.AddRange(
            new Ship { CargoCapacity = 10000, DockingSpace = "Dock A" },
            new Ship { CargoCapacity = 15000, DockingSpace = "Dock B" },
            new Ship { CargoCapacity = 20000, DockingSpace = "Dock C" }
        );
        dbContext.SaveChanges();
    }
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "ships",
    pattern: "ships",
    defaults: new { controller = "Ship", action = "Index" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
