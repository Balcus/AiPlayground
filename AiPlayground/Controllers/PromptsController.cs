using AiPlayground.BusinessLogic.Dtos;
using AiPlayground.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace NetRomApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PromptsController : Controller
{
    private readonly IPromptService _promptService;

    public PromptsController(IPromptService promptService)
    {
        _promptService = promptService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var prompts = await _promptService.GetAllPromptsAsync();
        return Ok(prompts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var prompt = await _promptService.GetPromptByIdAsync(id);
        if (prompt == null)
        {
            return NotFound();
        }
        
        return Ok(prompt);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PromptCreateDto promptCreateDto)
    {
        var createdPrompt = await _promptService.CreatePromptAsync(promptCreateDto);
        return Ok(createdPrompt);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _promptService.DeletePromptAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}