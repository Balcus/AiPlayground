using AiPlayground.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace NetRomApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlatformsController : Controller
{
    private readonly IPlatformService _platformService;
    public PlatformsController (IPlatformService platformService)
    {
        _platformService = platformService;
    }

    /// <summary>
    /// Fetches all platforms
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAllPlatformsAsync()
    {
        var platforms = await _platformService.GetAllPlatformsAsync();
        return Ok(platforms);
        
    }

    /// <summary>
    /// Fetches a platform by id
    /// </summary>
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