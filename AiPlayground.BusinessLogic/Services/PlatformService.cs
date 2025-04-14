using AiPlayground.BusinessLogic.Dtos;
using AiPlayground.BusinessLogic.Interfaces;
using AiPlayground.DataAccess.Entities;
using AiPlayground.DataAccess.Repositories;

namespace AiPlayground.BusinessLogic.Services;

public class PlatformService : IPlatformService
{
    private readonly IRepository<Platform> _platformRepository;

    public PlatformService(IRepository<Platform> platformRepository)
    {
        _platformRepository = platformRepository;
    }
    
    public async Task<IEnumerable<PlatformDto>> GetAllPlatformsAsync()
    {
        var platforms = await _platformRepository.GetAllAsync();

        return platforms.Select(p => new PlatformDto
        {
            Id = p.Id,
            Name = p.Name,
            ImageUrl = p.ImageUrl,
        });
    }

    public Task<PlatformDto> GetPlatformByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<PlatformDto> CreatePlatformAsync(PlatformCreateDto platformCreateDto)
    {
        throw new NotImplementedException();
    }

    public Task<PlatformDto> UpdatePlatformAsync(int id, PlatformCreateDto platformCreateDto)
    {
        throw new NotImplementedException();
    }

    public Task DeletePlatformAsync(int id)
    {
        throw new NotImplementedException();
    }
}