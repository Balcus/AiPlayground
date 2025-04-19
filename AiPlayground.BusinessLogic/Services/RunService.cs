using AiPlayground.BusinessLogic.Dtos;
using AiPlayground.BusinessLogic.Interfaces;
using AiPlayground.DataAccess.Entities;

namespace AiPlayground.BusinessLogic.Services;

public class RunService : IRunService
{
    public Task<IEnumerable<Run>> GetRunsByPromptIdAsync(int promptId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Run>> GetRunsByModelIdAsync(int modelId)
    {
        throw new NotImplementedException();
    }

    public Task<RunDto> UpdateScopeAsync(int id, RunUpdateDto scopeCreateDto)
    {
        throw new NotImplementedException();
    }
}