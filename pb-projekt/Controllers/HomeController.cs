    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using pb_projekt.Data;
    using pb_projekt.Models;
    using System.Threading.Tasks;

    namespace pb_projekt.Controllers
    {
        public class HomeController : Controller
        {
            private readonly AppDbContext _appDbContext;

            public HomeController(AppDbContext appDbContext)
            {
                _appDbContext = appDbContext;
            }

            [HttpGet]
            [Route("/", Name = "Home")]
            public IActionResult Index()
            {
                var ships = _appDbContext.Ships.ToList();
                return View(ships);
            }

            [HttpGet]
            public async Task<IActionResult> Details(int id)
            {
                var ship = await _appDbContext.Ships
                    .Include(s => s.Cargoes) // Eagerly load the related cargoes
                    .FirstOrDefaultAsync(s => s.Id == id);

                if (ship == null)
                {
                    return NotFound();
                }

                return View(ship);
            }
        }
    }
