using AiPlayground.BusinessLogic.Dtos;
using AiPlayground.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace NetRomApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ModelsController : Controller
{
    private readonly IPlatformService _platformService;

    public ModelsController (IPlatformService platformService)
    {
        _platformService = platformService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPlatformsAsync()
    {
        var platforms = await _platformService.GetAllPlatformsAsync();
        return Ok(platforms);
        
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPlatformByIdAsync(int id)
    {
        var platform = await _platformService.GetPlatformByIdAsync(id);
        if (platform == null)
        {
            return NotFound();
        }
        return Ok(platform);
    }
}