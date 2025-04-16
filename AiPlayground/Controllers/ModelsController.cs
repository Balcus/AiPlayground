using AiPlayground.BusinessLogic.Dtos;
using AiPlayground.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace NetRomApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ModelsController : Controller
{
    private readonly IModelService _modelService;

    public ModelsController(IModelService modelService)
    {
        _modelService = modelService;
    }

    [HttpGet]
    public async Task<IActionResult> GetModels()
    {
        var platforms = await _modelService.GetAllModelsAsync();
        return Ok(platforms);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetModelByIdAsync(int id)
    {
        var platform = await _modelService.GetModelByIdAsync(id);
        if (platform == null)
        {
            return NotFound();
        }
        
        return Ok(platform);
    }
}