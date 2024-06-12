using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pb_projekt.Data;
using pb_projekt.Models;
using System.Linq;
using System.Threading.Tasks;

namespace pb_projekt.Controllers
{
    public class ShipController : Controller
    {
        private readonly AppDbContext _context;

        public ShipController(AppDbContext context)
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

            return View(ship);
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
    }
}
