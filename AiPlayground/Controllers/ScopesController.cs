
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
        private readonly IPromptService _promptService;
        
        public ScopesController(IScopeService scopeService, IPromptService promptService)
        {
            _scopeService = scopeService;
            _promptService = promptService;
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

        [HttpGet("{id}/prompts")]
        public async Task<IActionResult> GetPromptsById(int id)
        {
            var scope = await _scopeService.GetScopeByIdAsync(id);
            if (scope == null)
            {
                return NotFound();
            }
            
            var prompts = await _promptService.GetPromptsByScopeIdAsync(id);
            return Ok(prompts);
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

        // TODO: make sure this is ok as I changed signature from Put(ScopeDto scopeDto) -> Put(int id, ScopeCreateDto scopeDto)
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ScopeCreateDto scopeDto)
        {
            var updatedScope = await _scopeService.UpdateScopeAsync(id, scopeDto);
            return Ok(updatedScope);
        }
    }