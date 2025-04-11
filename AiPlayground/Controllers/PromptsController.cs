using Microsoft.AspNetCore.Mvc;

namespace NetRomApp.Controllers;

public class PromptsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}