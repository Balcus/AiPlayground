using AiPlayground.BusinessLogic.Dto;
using AiPlayground.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace NetRomApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RunsController : Controller
{
    private readonly IRunService _runService;
    public RunsController(IRunService runService)
    {
        _runService = runService;
    }

    /// <summary>
    /// Returns all runs
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var runs = await _runService.GetAllAsync();
        return Ok(runs);
    }

    /// <summary>
    /// Creates a new Run
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateRuns([FromBody] RunCreateDto runCreateDto)
    {
        if (runCreateDto.ModelsToRun.Count == 0)
        {
            return BadRequest("Invalid Data");
        }

        var runs = await _runService.CreateRunsAsync(runCreateDto);
        return Ok(runs);
    }

    /// <summary>
    /// Patches a run's user rating
    /// </summary>
    [HttpPatch("{runId}")]
    public async Task<IActionResult> PatchRun(int runId, [FromBody] RunUpdateDto runUpdateDto)
    {
        var run = await _runService.GetRunByIdAsync(runId);
        
        if (run == null)
        {
            return NotFound();
        }

        var updatedScope = await _runService.UpdateRunAsync(runId, runUpdateDto);
        return Ok(updatedScope);
    }

    /// <summary>
    /// Fetches all runs for a prompt given its ID
    /// </summary>
    [HttpGet("/api/prompts/{promptId}/runs")]
    public async Task<IActionResult> GetRunsForPromptAsync(int promptId)
    {
        var runs = await _runService.GetRunsForPromptAsync(promptId);
        return Ok(runs);
    }

    /// <summary>
    /// Fetches all runs for a model given its ID
    /// </summary>
    [HttpGet("/api/models/{modelId}/runs")]
    public async Task<IActionResult> GetRunsForModelAsync(int modelId)
    {
        var runs = await _runService.GetRunsForModelAsync(modelId);
        return Ok(runs);
    }
}