using AiPlayground.BusinessLogic.Dtos;
using AiPlayground.BusinessLogic.Interfaces;
using AiPlayground.DataAccess.Entities;
using AiPlayground.DataAccess.Repositories;

namespace AiPlayground.BusinessLogic.Services;

public class ScopeService : IScopeService
{
    private readonly IRepository<Scope> _scopeRepository;

    public ScopeService(IRepository<Scope> scopeRepository)
    {
        _scopeRepository = scopeRepository;
    }

    public async Task<IEnumerable<ScopeDto>> GetAllScopesAsync()
    {
        var scopes = await _scopeRepository.GetAllAsync();

        return scopes.Select(scope => new ScopeDto
        {
            Id = scope.Id,
            Name = scope.Name,
        });
    }

    public async Task<ScopeDto?> GetScopeByIdAsync(int id)
    {
        var scope = await _scopeRepository.GetByIdAsync(id);

        if (scope == null)
        {
            return null;
        }
        
        return new ScopeDto
        {
            Id = scope.Id,
            Name = scope.Name,
        };
    }

    public async Task<ScopeDto> CreateScopeAsync(ScopeCreateDto scopeDto)
    {
        var scope = new Scope
        {
            Name = scopeDto.Name,
        };

        var createdScope = await _scopeRepository.AddAsync(scope);

        return new ScopeDto
        {
            Id = createdScope.Id,
            Name = createdScope.Name,
        };
    }

    public async Task<ScopeDto> UpdateScopeAsync(int id, ScopeCreateDto scopeDto)
    {
        var scope = new Scope
        {
            Id = id,
            Name = scopeDto.Name,
        };
        
        var updatedScope = await _scopeRepository.UpdateAsync(scope);

        return new ScopeDto
        {
            Id = updatedScope.Id,
            Name = updatedScope.Name,
        };

    }

    public async Task DeleteScopeAsync(int id)
    {
        await _scopeRepository.DeleteAsync(id);
    }
}