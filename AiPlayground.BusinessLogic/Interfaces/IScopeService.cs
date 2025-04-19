using AiPlayground.BusinessLogic.Dtos;

namespace AiPlayground.BusinessLogic.Interfaces;

public interface IScopeService
{
    Task<IEnumerable<ScopeDto>> GetAllScopesAsync();
    Task<ScopeDto?> GetScopeByIdAsync(int id);
    Task<ScopeDto> CreateScopeAsync(ScopeCreateDto scopeCreateDto);
    Task<ScopeDto> UpdateScopeAsync(int id, ScopeCreateDto scopeCreateDto);
    Task DeleteScopeAsync(int id);
    Task<IEnumerable<PromptDto>> GetPromptsByScopeIdAsync(int scopeId);
}