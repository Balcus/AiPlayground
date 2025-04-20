using AiPlayground.BusinessLogic.Dtos;
using AiPlayground.DataAccess.Entities;

namespace AiPlayground.BusinessLogic.Interfaces;

public interface IRunService
{
    Task<IEnumerable<RunDto>> GetRunsByPromptIdAsync(int promptId);
    Task<IEnumerable<RunDto>> GetRunsByModelIdAsync(int modelId);
    Task<RunDto?> UpdateScopeAsync(int id, RunUpdateDto scopeCreateDto);
}