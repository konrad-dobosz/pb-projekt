    using Microsoft.AspNetCore.Mvc;

    namespace pb_projekt.Controllers
    {
        public class HomeController : Controller
        {
            [HttpGet]
            [Route("/", Name = "Home")]
            public IActionResult Index()
            {
                return RedirectToRoute("Ships");
            }
        }
    }
