using AiPlayground.BusinessLogic.Dtos;
using AiPlayground.BusinessLogic.Interfaces;
using AiPlayground.DataAccess.Entities;
using AiPlayground.DataAccess.Repositories;

namespace AiPlayground.BusinessLogic.Services;

public class RunService : IRunService
{
    private readonly RunRepository _runRepository;

    public RunService(RunRepository runRepository)
    {
        _runRepository = runRepository;
    }
    
    public async Task<IEnumerable<RunDto>> GetRunsByPromptIdAsync(int promptId)
    {
        var runs = await _runRepository.GetAll();
        var runsForPrompt = runs.Where(r => r.PromptId == promptId);

        return runsForPrompt.Select(r => new RunDto
        {
            Id = r.Id,
            ActualResponse = r.ActualResponse,
            ModelId = r.ModelId,
            PromptId = r.PromptId,
            Rating = r.Rating,
            Temp = r.Temp,
            UserRating = r.UserRating,
        });
    }

    public async Task<IEnumerable<RunDto>> GetRunsByModelIdAsync(int modelId)
    {
        var runs = await _runRepository.GetAll();
        var runsForPrompt = runs.Where(r => r.ModelId == modelId);

        return runsForPrompt.Select(r => new RunDto
        {
            Id = r.Id,
            ActualResponse = r.ActualResponse,
            ModelId = r.ModelId,
            PromptId = r.PromptId,
            Rating = r.Rating,
            Temp = r.Temp,
            UserRating = r.UserRating,
        });
    }

    public async Task<RunDto?> UpdateScopeAsync(int id, RunUpdateDto runUpdateDto)
    {
        var existingRun = await _runRepository.GetByIdAsync(id);
    
        if (existingRun == null)
        {
            return null;
        }
    
        existingRun.UserRating = runUpdateDto.UserRating;
    
        await _runRepository.UpdateAsync(existingRun);
    
        return new RunDto
        {
            Id = existingRun.Id,
            ActualResponse = existingRun.ActualResponse,
            ModelId = existingRun.ModelId,
            PromptId = existingRun.PromptId,
            Rating = existingRun.Rating,
            Temp = existingRun.Temp,
            UserRating = existingRun.UserRating
        };
    }
}