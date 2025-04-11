using Microsoft.AspNetCore.Mvc;

namespace NetRomApp.Controllers;

public class RunsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}