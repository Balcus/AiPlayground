
using AiPlayground.BusinessLogic.Dtos;
using AiPlayground.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace NetRomApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ScopesController : ControllerBase
{
    private readonly IScopeService _scopeService;
    
    public ScopesController(IScopeService scopeService)
    {
        _scopeService = scopeService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var scopes = await _scopeService.GetAllScopesAsync();
        return Ok(scopes);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ScopeCreateDto scopeDto)
    {
        if (scopeDto == null)
        {
            throw new Exception("scopeCreateDto is null");
        }

        var createdScope = await _scopeService.CreateScopeAsync(scopeDto);
        return Ok(createdScope);

    }
}