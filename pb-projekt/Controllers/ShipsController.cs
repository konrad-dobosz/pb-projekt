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
            var ships = await _context.Ships.ToListAsync();
            return View(ships);
        }
        
        [HttpGet]
        public async Task<IActionResult> Add()
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
        
        [HttpPost]
        public async Task<IActionResult> Remove(int shipId)
        {
            _context.Ships.Remove(
                    new Ship()
                    {
                        Id = shipId
                    }
            );
            await _context.SaveChangesAsync();
            return RedirectToRoute("Ships");
        }
    }
}
