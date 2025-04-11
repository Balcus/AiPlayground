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

        return scopes.Select(scope => new ScopeDto()
        {
            Id = scope.Id,
            Name = scope.Name,
        });
    }

    public Task<ScopeDto> GetScopeByIsAsync()
    {
        throw new NotImplementedException();
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

    public Task<ScopeDto> UpdateScopeAsync(ScopeDto scopeDto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteScopeAsync(int id)
    {
        throw new NotImplementedException();
    }
}