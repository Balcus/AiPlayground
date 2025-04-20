using AiPlayground.BusinessLogic.Dtos;
using AiPlayground.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace NetRomApp.Controllers;

[Route("api")]
[ApiController]
public class RunsController : ControllerBase
{
    private readonly IRunService _runService;
    public RunsController(IRunService runService)
    {
        _runService = runService;
    }
    
    /// <summary>
    /// Retrieves all runs for a specific prompt
    /// </summary>
    /// <returns>A collection of run data transfer objects</returns>
    [HttpGet("/prompts/{promptId}/runs")]
    public async Task<IActionResult> GetRunsByPromptId(int promptId)
    {
        var runs = await _runService.GetRunsByPromptIdAsync(promptId);
        return Ok(runs);
    }

    /// <summary>
    /// Retrieves all runs for a specific model
    /// </summary>
    /// <returns>A collection of run data transfer objects</returns>
    [HttpGet("/models/{modelId}/runs")]
    public async Task<IActionResult> GetRunsByModelId(int modelId)
    {
        var runs = await _runService.GetRunsByModelIdAsync(modelId);
        return Ok(runs);
    }

    /// <summary>
    /// Updates the user rating for a run
    /// </summary>
    /// <returns>The updated run if the run with given id exists</returns>
    [HttpPut("/runs/{id}")]
    public async Task<IActionResult> UpdateRunAsync(int id, RunUpdateDto runUpdateDto)
    {
        var updatedRun = await _runService.UpdateScopeAsync(id, runUpdateDto);
        if (updatedRun == null)
        {   
            return NotFound();
        }
        return Ok(updatedRun);
    }
}