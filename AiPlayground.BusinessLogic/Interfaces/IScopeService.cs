using AiPlayground.BusinessLogic.Dtos;

namespace AiPlayground.BusinessLogic.Interfaces;

public interface IScopeService
{
    Task<IEnumerable<ScopeDto>> GetAllScopesAsync();
    Task<ScopeDto> GetScopeByIsAsync();
    Task<ScopeDto> CreateScopeAsync(ScopeCreateDto scopeDto);
    Task<ScopeDto> UpdateScopeAsync(ScopeDto scopeDto);
    Task DeleteScopeAsync(int id);
}