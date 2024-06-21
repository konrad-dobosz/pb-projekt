﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pb_projekt.Data;
using pb_projekt.Models;
using System.Linq;
using System.Threading.Tasks;

namespace pb_projekt.Controllers
{
    public class LandShipmentsController : Controller
    {
        private readonly AppDbContext _context;

        public LandShipmentsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("/landshipments", Name = "LandShipments")]
        public async Task<IActionResult> Index()
        {
            var landShipments = await _context.LandShipments.Include(l => l.Vehicles).ToListAsync();
            return View(landShipments);
        }

        [HttpGet]
        [Route("/landshipments/{id}/cargo", Name = "LandShipmentCargo")]
        public async Task<IActionResult> Cargo(int id)
        {
            var landShipment = await _context.LandShipments
                .Include(ls => ls.Cargoes)  
                .FirstOrDefaultAsync(ls => ls.Id == id);

            if (landShipment == null)
            {
                return NotFound();
            }

            return View(landShipment);
        }


    }
}
