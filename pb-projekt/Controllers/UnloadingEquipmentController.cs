using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pb_projekt.Data;

namespace pb_projekt.Controllers;

public class UnloadingEquipmentController : Controller
{
    private readonly AppDbContext _context;

    public UnloadingEquipmentController(AppDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    [Route("/unloading_equipment", Name = "UnloadingEquipment")]
    public async Task<IActionResult> Index()
    {
        var unloadingEquipment = await _context.UnloadingEquipments.ToListAsync();
        return View(unloadingEquipment);
    }
}