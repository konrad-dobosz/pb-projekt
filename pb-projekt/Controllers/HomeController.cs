using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pb_projekt.Data;
using pb_projekt.Models;

namespace pb_projekt.Controllers;

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
        // User user = new User { Name = "Test", Surname = "Test 2", Password = "123", Email = "test@test.pl" };
        // _appDbContext.Users.Add(user);
        // _appDbContext.SaveChanges();
        return View();
    }
}