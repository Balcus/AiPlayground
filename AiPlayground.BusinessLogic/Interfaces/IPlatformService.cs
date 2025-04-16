using AiPlayground.BusinessLogic.Dtos;

namespace AiPlayground.BusinessLogic.Interfaces;

public interface IPlatformService
{
    Task<IEnumerable<PlatformDto>> GetAllPlatformsAsync();
    Task<PlatformDto?> GetPlatformByIdAsync(int id);
    Task<PlatformDto> CreatePlatformAsync(PlatformCreateDto platformCreateDto);
    Task<PlatformDto> UpdatePlatformAsync(int id, PlatformCreateDto platformCreateDto);
    Task DeletePlatformAsync(int id);
}