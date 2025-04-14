
    using AiPlayground.BusinessLogic.Dtos;
    using AiPlayground.BusinessLogic.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    // TODO: consider catching the errors that can arrise from each method and return a corrrect status code for each
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
        public async Task<IActionResult> Post([FromBody] ScopeCreateDto? scopeDto)
        {
            if (scopeDto == null)
            {
                return BadRequest();
            }

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