using AiPlayground.BusinessLogic.Dto;
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
}