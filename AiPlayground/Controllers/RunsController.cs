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
    
    [HttpGet("/prompts/{promptId}/runs")]
    public async Task<IActionResult> GetRunsByPromptId(int promptId)
    {
        var runs = await _runService.GetRunsByPromptIdAsync(promptId);
        return Ok(runs);
    }

    [HttpGet("/models/{modelId}/runs")]
    public async Task<IActionResult> GetRunsByModelId(int modelId)
    {
        var runs = await _runService.GetRunsByModelIdAsync(modelId);
        return Ok(runs);
    }

    [HttpPost("/runs/{id}")]
    public async Task<IActionResult> UpdateRunAsync(int id, RunUpdateDto runUpdateDto)
    {
        var updatedRun = await _runService.UpdateScopeAsync(id, runUpdateDto);
        return Ok(updatedRun);
    }
}