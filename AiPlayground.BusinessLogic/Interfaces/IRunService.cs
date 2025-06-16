using AiPlayground.BusinessLogic.Dto;
using AiPlayground.DataAccess.Entities;
using OpenAI.Assistants;

namespace AiPlayground.BusinessLogic.Interfaces;

public interface IRunService
{
    Task<List<RunDto>> GetAllAsync();
    Task<List<RunDto>> CreateRunsAsync(RunCreateDto runCreateDto);
    Task<RunDto?> GetRunByIdAsync(int runId);
    Task<RunDto?> UpdateRunAsync(int id, RunUpdateDto runUpdateDto);
    Task<List<RunDto>> GetRunsForPromptAsync(int promptId);
    Task<List<RunDto>> GetRunsForModelAsync(int modelId);
}