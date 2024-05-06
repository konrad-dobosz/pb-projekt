using Microsoft.AspNetCore.Mvc;
using pb_projekt.Data;
using pb_projekt.Models;

namespace pb_projekt.Controllers;

public class AuthController : Controller
{
    private readonly AppDbContext _appDbContext;

    public AuthController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    
    [HttpGet]
    public ActionResult Login()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<ActionResult> Login(string Email, string Password)
    {
        Console.WriteLine(Email);
        Console.WriteLine(Password);
        return View();
    }
}