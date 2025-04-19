
    using AiPlayground.BusinessLogic.Dtos;
    using AiPlayground.BusinessLogic.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    // TODO: consider catching the errors that can arise from each method and return a corrrect status code for each
    namespace NetRomApp.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class ScopesController : ControllerBase
    {
        private readonly IScopeService _scopeService;
        
        public ScopesController(IScopeService scopeService, IPromptService promptService)
        {
            _scopeService = scopeService;
        }
        
        [HttpGet("{scopeId}/prompts")]
        public async Task<IActionResult> GetPromptsByScopeIdAsync(int scopeId)
        {
            var prompts = await _scopeService.GetPromptsByScopeIdAsync(scopeId);

            return Ok(prompts);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var scopes = await _scopeService.GetAllScopesAsync();
            return Ok(scopes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var scope = await _scopeService.GetScopeByIdAsync(id);
            if (scope == null)
            {
                return NotFound();
            }
            return Ok(scope);
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ScopeCreateDto scopeDto)
        {
            var createdScope = await _scopeService.CreateScopeAsync(scopeDto);
            return Ok(createdScope);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _scopeService.DeleteScopeAsync(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ScopeCreateDto scopeDto)
        {
            var updatedScope = await _scopeService.UpdateScopeAsync(id, scopeDto);
            return Ok(updatedScope);
        }
    }