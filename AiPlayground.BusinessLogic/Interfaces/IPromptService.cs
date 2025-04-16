

using AiPlayground.BusinessLogic.Dtos;

namespace AiPlayground.BusinessLogic.Interfaces;

public interface IPromptService
{
    Task<IEnumerable<PromptDto>> GetAllPromptsAsync();
    Task<PromptDto?> GetPromptByIdAsync(int id);
    Task<PromptDto> CreatePromptAsync(PromptCreateDto promptCreateDto);
    Task<PromptDto> UpdatePromptAsync(int id, PromptCreateDto promptCreateDto);
    Task<IEnumerable<PromptDto>> GetPromptsByScopeIdAsync(int scopeId);
    Task DeletePromptAsync(int id);
}