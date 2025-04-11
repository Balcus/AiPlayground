using Microsoft.AspNetCore.Mvc;

namespace NetRomApp.Controllers;

public class ModelsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}