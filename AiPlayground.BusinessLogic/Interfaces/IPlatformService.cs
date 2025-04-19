using AiPlayground.BusinessLogic.Dtos;

namespace AiPlayground.BusinessLogic.Interfaces;

public interface IPlatformService
{
    Task<IEnumerable<PlatformDto>> GetAllPlatformsAsync();
    Task<PlatformDto?> GetPlatformByIdAsync(int id);
}