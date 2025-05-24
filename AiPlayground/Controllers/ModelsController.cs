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

    /// <summary>
    /// Fetches all models
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetModelsAsync()
    {
        var platforms = await _modelService.GetAllModelsAsync();
        return Ok(platforms);
    }

    /// <summary>
    /// Fetches a model by id
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetModelByIdAsync(int id)
    {
        var model = await _modelService.GetModelByIdAsync(id);
        if (model == null)
        {
            return NotFound();
        }
        
        return Ok(model);
    }
}