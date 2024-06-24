using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pb_projekt.Data;
using pb_projekt.Models;
using System.Linq;
using System.Threading.Tasks;

namespace pb_projekt.Controllers
{
    public class ShipsController : Controller
    {
        private readonly AppDbContext _context;

        public ShipsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/ships", Name = "Ships")]
        public async Task<IActionResult> Index()
        {
            var ships = await _context.Ships.Include(s => s.UnloadingEquipments).ToListAsync();
            return View(ships);
        }

        [HttpGet]
        [Route("/ships/{id}/cargo", Name = "ShipCargo")]
        public async Task<IActionResult> Cargo(int id)
        {
            var ship = await _context.Ships
                .Include(s => s.Cargoes)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (ship == null)
            {
                return NotFound();
            }

            ViewBag.Hangars = await _context.Hangars.ToListAsync();

            return View("Cargo/Index", ship);
        }
        
        [HttpGet]
        [Route("/ships/{id}/cargo/add", Name = "AddCargo")]
        public async Task<IActionResult> AddCargo(int id)
        {
            var ship = await _context.Ships
                .Include(s => s.Cargoes)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (ship == null)
            {
                return NotFound();
            }
            
            return View("Cargo/Add", ship);
        }

        [HttpPost]
        [Route("/ships/{id}/cargo/add")]
        public async Task<IActionResult> AddCargo(int shipId, string serialNumber, string securityLevel, string weight, string cargoType, string destinationPort)
        {
            _context.Cargoes.Add(
                new Cargo()
                {
                    SerialNumber = serialNumber,
                    SecurityLevel = securityLevel,
                    Weight = double.Parse(weight),
                    CargoType = cargoType,
                    DestinationPort = destinationPort,
                    ShipId = shipId
                });
            await _context.SaveChangesAsync();
            return RedirectToRoute("ShipCargo", new { id = shipId });
        }

        [HttpGet]
        [Route("/ships/{id}/unloading-equipment", Name = "ShipUnloadingEquipment")]
        public async Task<IActionResult> UnloadingEquipment(int id)
        {
            var ship = await _context.Ships
                .Include(s => s.UnloadingEquipments)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (ship == null)
            {
                return NotFound();
            }

            return View(ship.UnloadingEquipments.ToList());
        }

        [HttpGet]
        [Route("/ships/add", Name = "AddShip")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(double capacity, string dock)
        {
            _context.Ships.Add(
                new Ship()
                {
                    CargoCapacity = capacity,
                    DockingSpace = dock
                });
            await _context.SaveChangesAsync();
            return RedirectToRoute("Ships");
        }
        
        [HttpGet]
        [Route("/ships/{id}/edit", Name = "ShipEdit")]
        public async Task<IActionResult> Edit(int id)
        {
            var ship = await _context.Ships.FindAsync(id);

            if (ship == null)
            {
                return NotFound();
            }

            return View(ship);
        }
        
        [HttpPost]
        [Route("/ships/{id}/edit")]
        public async Task<IActionResult> Edit(int shipId, double capacity, string dock)
        {
            var edited = new Ship() { Id = shipId, CargoCapacity = capacity, DockingSpace = dock };

            _context.Ships.Update(edited);
            await _context.SaveChangesAsync();
            return RedirectToRoute("Ships");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int shipId)
        {
            var ship = await _context.Ships.FindAsync(shipId);
            if (ship != null)
            {
                _context.Ships.Remove(ship);
                await _context.SaveChangesAsync();
            }
            return RedirectToRoute("Ships");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/ships/{id}/cargo")]
        public async Task<IActionResult> TransferCargo(int cargoId, int hangarId)
        {
            var cargo = await _context.Cargoes.FindAsync(cargoId);
            if (cargo == null)
            {
                return NotFound();
            }

            cargo.ShipId = null;
            cargo.HangarId = hangarId;
            var hangar = await _context.Hangars.FindAsync(hangarId);
            hangar.Cargoes.Add(cargo);
            await _context.SaveChangesAsync();

            return RedirectToAction("");
        }

    }
}
