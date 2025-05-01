

using AiPlayground.BusinessLogic.Dto;

namespace AiPlayground.BusinessLogic.Interfaces;

public interface IPromptService
{
    Task<IEnumerable<PromptDto>> GetAllPromptsAsync();
    Task<PromptDto?> GetPromptByIdAsync(int id);
    Task<PromptDto> CreatePromptAsync(PromptCreateDto promptCreateDto);
    Task DeletePromptAsync(int id);
}