using AiPlayground.BusinessLogic.Dtos;
using AiPlayground.DataAccess.Entities;

namespace AiPlayground.BusinessLogic.Interfaces;

public interface IRunService
{
    Task<IEnumerable<Run>> GetRunsByPromptIdAsync(int promptId);
    Task<IEnumerable<Run>> GetRunsByModelIdAsync(int modelId);
    Task<RunDto> UpdateScopeAsync(int id, RunUpdateDto scopeCreateDto);
}