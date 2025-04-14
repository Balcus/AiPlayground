using AiPlayground.BusinessLogic.Dtos;

namespace AiPlayground.BusinessLogic.Interfaces;

public interface IScopeService
{
    Task<IEnumerable<ScopeDto>> GetAllScopesAsync();
    Task<ScopeDto?> GetScopeByIdAsync(int id);
    Task<ScopeDto> CreateScopeAsync(ScopeCreateDto scopeDto);
    Task<ScopeDto> UpdateScopeAsync(int id, ScopeCreateDto scopeDto);
    Task DeleteScopeAsync(int id);
}