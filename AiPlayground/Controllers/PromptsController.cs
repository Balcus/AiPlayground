using AiPlayground.BusinessLogic.Dtos;
using AiPlayground.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace NetRomApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PromptsController : Controller
{
    private readonly IPromptService _promptsService;

    public PromptsController(IPromptService promptsService)
    {
        _promptsService = promptsService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var prompts = await _promptsService.GetAllPromptsAsync();
        return Ok(prompts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var prompt = await _promptsService.GetPromptByIdAsync(id);
        if (prompt == null)
        {
            return NotFound();
        }
        
        return Ok(prompt);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PromptCreateDto promptCreateDto)
    {
        var createdPrompt = await _promptsService.CreatePromptAsync(promptCreateDto);
        return Ok(createdPrompt);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _promptsService.DeletePromptAsync(id);
        return Ok();
    }
}