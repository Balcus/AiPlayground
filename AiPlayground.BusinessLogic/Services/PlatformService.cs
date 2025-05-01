using AiPlayground.BusinessLogic.Dto;
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
            Models = p.Models.Select(m => new ModelDto
            {
                Id = m.Id,
                Name = m.Name,
                AverageRating = 0,
            }).ToList(),
        });
    }

    public async Task<PlatformDto?> GetPlatformByIdAsync(int id)
    {
        var platform = await _platformRepository.GetByIdAsync(id);
        if (platform == null)
        {
            return null;
        }

        return new PlatformDto
        {
            Id = platform.Id,
            Name = platform.Name,
            Models = platform.Models.Select(m => new ModelDto
            {
                Id = m.Id,
                Name = m.Name,
                AverageRating = 0,
            }).ToList(),
        };
    }
}