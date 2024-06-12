using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using pb_projekt.Data;
using pb_projekt.Models;

namespace pb_projekt.Controllers;

public class AuthController(SignInManager<User> signInManager, UserManager<User> userManager) : Controller
{
    
    [HttpGet]
    [Route("/auth/login", Name = "Login")]
    public ActionResult Login()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<ActionResult> Login(string Email, string Password)
    {
        var result = await signInManager.PasswordSignInAsync(Email, Password, false, false);
        if (result.Succeeded)
        {
            Console.WriteLine("OK");
            return RedirectToRoute("Home");
        }
        Console.WriteLine(Email);
        Console.WriteLine(Password);
        
        return View();
    }

    [HttpGet]
    [Route("/auth/logout")]
    public async Task<ActionResult> Logout()
    {
        await signInManager.SignOutAsync();

        return RedirectToRoute("Login");
    }
    
    [HttpGet]
    public ActionResult Register()
    {
        return View();
    }


    [HttpPost]
    public async Task<ActionResult> Register(string Email, string Password)
    {
        User u = new()
        {
            UserName = Email,
            Email = Email,
        };

        var result = await userManager.CreateAsync(u, Password);

        if (result.Succeeded)
        {
            await signInManager.SignInAsync(u, false);

            Console.WriteLine("OK");
            return RedirectToRoute("Home");
        }
        
        return View();
    }
}