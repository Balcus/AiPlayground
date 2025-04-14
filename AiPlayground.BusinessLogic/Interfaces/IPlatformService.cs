using AiPlayground.BusinessLogic.Dtos;

namespace AiPlayground.BusinessLogic.Interfaces;

public interface IPlatformService
{
    Task<IEnumerable<PlatformDto>> GetAllPlatformsAsync();
    Task<PlatformDto> GetPlatformByIdAsync(int id);
    Task<PlatformDto> CreatePlatformAsync(PlatformCreateDto scopeDto);
    Task<PlatformDto> UpdatePlatformAsync(int id, PlatformCreateDto scopeDto);
    Task DeletePlatformAsync(int id);
}