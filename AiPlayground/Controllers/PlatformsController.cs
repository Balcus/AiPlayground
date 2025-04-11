using Microsoft.AspNetCore.Mvc;

namespace NetRomApp.Controllers;

public class PlatformsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}