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
            if (platformType == PlatformType.OpenAI)
            {
                var run = await CreateRunAsync(model, prompt, modelToRun, modelToRun.Temperature);
                runs.Add(run);
            }
        }
        return runs;
    }

    private async Task<RunDto> CreateRunAsync(Model model, Prompt prompt, ModelRunDto modelToRun, object temperature)
    {
        ChatClient chatClient = new(model: model.Name, apiKey: Environment.GetEnvironmentVariable("OPENAI_API_KEY"));

        var sysMsg = new SystemChatMessage(prompt.SystemMsg);
        var usrMsg = new UserChatMessage(prompt.UserMessage);

        var message = new List<ChatMessage>
        {
            sysMsg,
            usrMsg
        };

        var options = new ChatCompletionOptions
        {
            Temperature = (float)temperature,
        };
        
        ChatCompletion reChatCompletion = await chatClient.CompleteChatAsync(message, options);
        var actualResponse = reChatCompletion.Content.First().Text;
        var run = await CreateRun(model.Id, prompt.Id, modelToRun, actualResponse, (double)temperature, 0);

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