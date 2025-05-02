using AiPlayground.BusinessLogic.AiClient;
using AiPlayground.BusinessLogic.Dto;
using AiPlayground.BusinessLogic.Enums;
using AiPlayground.BusinessLogic.Interfaces;
using AiPlayground.DataAccess.Entities;
using AiPlayground.DataAccess.Repositories;
using OpenAI.Chat;

namespace AiPlayground.BusinessLogic.Services;

public class RunService : IRunService
{
    private readonly IRepository<Run> _runRepository;
    private readonly IRepository<Model> _modelRepository;
    private readonly IRepository<Prompt> _promptRepository;
    
    public RunService(IRepository<Run> runRepository, IRepository<Model> modelRepository, IRepository<Prompt> promptRepository)
    {
        _runRepository = runRepository;
        _modelRepository = modelRepository;
        _promptRepository = promptRepository;
    }


    public async Task<List<RunDto>> CreateRunsAsync(RunCreateDto runCreateDto)
    {
        var runs = new List<RunDto>();
        var prompt = await _promptRepository.GetByIdAsync(runCreateDto.PromptId);

        if (prompt == null)
        {
            throw new Exception("Prompt with not found");
        }

        foreach (var modelToRun in runCreateDto.ModelsToRun)
        {
            var model = await _modelRepository.GetByIdAsync(modelToRun.ModelId);
            if (model == null)
            {
                throw new Exception("Model with not found");
            }
            
            var platformType = (PlatformType)model.PlatformId;
            if (platformType == PlatformType.OpenAi)
            {
                var run = await CreateRunAsync(model, prompt, modelToRun, modelToRun.Temperature);
                runs.Add(run);
            }
        }
        return runs;
    }

    private async Task<RunDto> CreateRunAsync(Model model, Prompt prompt, ModelRunDto modelToRun, double temperature)
    {
        AiClientFactory factory = new AiClientFactory();
        OpenAiClient client = (OpenAiClient)factory.GenerateClient(model);
        string response = await client.GenerateResponseAsync(prompt.SystemMsg, prompt.UserMessage, temperature);
        
        var run = await CreateRun(model.Id, prompt.Id, modelToRun, response, (double)temperature, 0);

        return new RunDto
        {
            Id = run.Id,
            ModelId = run.ModelId,
            PromptId = run.PromptId,
            ActualResponse = run.ActualResponse,
            Temp = run.Temp,
            Rating = run.Rating,
            UserRating = run.UserRating,
        };
    }

    private async Task<Run> CreateRun(int modelId, int promptId, ModelRunDto modelToRun, string actualResponse, double temperature, double rating)
    {
        var run = new Run
        {
            ModelId = modelId,
            PromptId = promptId,
            ActualResponse = actualResponse,
            Temp = temperature,
            Rating = rating,
        };
        
        await _runRepository.AddAsync(run);
        return run;
    }
}