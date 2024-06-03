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
            var ships = await _context.Ships.ToListAsync();
            return View(ships);
        }
    }
}
