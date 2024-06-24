using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pb_projekt.Data;
using pb_projekt.Models;
using System.Linq;
using System.Threading.Tasks;

namespace pb_projekt.Controllers
{
    public class HangarsController : Controller
    {
        private readonly AppDbContext _context;

        public HangarsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/hangars", Name = "Hangars")]
        public async Task<IActionResult> Index()
        {
            var hangars = await _context.Hangars.Include(h => h.Cargoes).ToListAsync();
            return View(hangars);
        }

        [HttpGet]
        [Route("/hangars/{id}/cargo", Name = "HangarCargo")]
        public async Task<IActionResult> Cargo(int id)
        {
            var hangar = await _context.Hangars
                .Include(h => h.Cargoes)
                .FirstOrDefaultAsync(h => h.Id == id);

            if (hangar == null)
            {
                return NotFound();
            }

            ViewBag.LandShipments = await _context.LandShipments.ToListAsync(); 

            return View(hangar.Cargoes);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/hangars/{id}/cargo")]
        public async Task<IActionResult> TransferCargo(int cargoId, int landShipmentId)
        {
            var cargo = await _context.Cargoes.FindAsync(cargoId);
            if (cargo == null)
            {
                return NotFound();
            }

            cargo.HangarId = null;
            cargo.LandShipmentId = landShipmentId;
            var landShipment = await _context.LandShipments.FindAsync(landShipmentId);
            landShipment.Cargoes.Add(cargo);
            await _context.SaveChangesAsync();

            return RedirectToAction("");
        }
    }

}
