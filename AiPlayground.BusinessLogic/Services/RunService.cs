using System.Diagnostics;
using AiPlayground.BusinessLogic.AiClient;
using AiPlayground.BusinessLogic.Dto;
using AiPlayground.BusinessLogic.Evaluator;
using AiPlayground.BusinessLogic.Interfaces;
using AiPlayground.DataAccess.Entities;
using AiPlayground.DataAccess.Repositories;

namespace AiPlayground.BusinessLogic.Services;

public class RunService : IRunService
{
    private readonly IRunRepository _runRepository;
    private readonly IRepository<Model> _modelRepository;
    private readonly IRepository<Prompt> _promptRepository;
    
    public RunService(IRunRepository runRepository, IRepository<Model> modelRepository, IRepository<Prompt> promptRepository)
    {
        _runRepository = runRepository;
        _modelRepository = modelRepository;
        _promptRepository = promptRepository;
    }

    public async Task<List<RunDto>> GetAllAsync()
    {

        var result = new List<RunDto>();
        var runs = await _runRepository.GetAllAsync();

        foreach (var run in runs )
        {
            result.Add(new RunDto
            {
                Id = run.Id,
                Rating = run.Rating,
                ActualResponse = run.ActualResponse,
                UserRating = run.UserRating,
                ModelId = run.ModelId,
                PromptId = run.PromptId,
            });
        }
        return result;
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
                throw new Exception("Model with the given Id not found");
            }
            
            var run = await CreateRunAsync(model, prompt, modelToRun, modelToRun.Temperature);
            runs.Add(run);
            
        }
        return runs;
    }

    public async Task<RunDto?> GetRunByIdAsync(int id)
    {
        
        var run = await _runRepository.GetByIdAsync(id);

        if (run == null)
        {
            return null;
        }

        return new RunDto
        {
            Id = run.Id,
            PromptId = run.PromptId,
            ModelId = run.ModelId,
            ActualResponse = run.ActualResponse,
            Temp = run.Temp,
            Rating = run.Rating,
            UserRating = run.UserRating,
        };
        
    }

    public async Task<RunDto?> UpdateRunAsync(int id, RunUpdateDto runUpdateDto)
    {
        var run  = await _runRepository.GetByIdAsync(id);
        if (run == null)
        {
            return null;
        }

        run.UserRating = runUpdateDto.UserRaing;
        var updatedRun = await _runRepository.UpdateAsync(run);

        return new RunDto()
        {
            Id = updatedRun.Id,
            ModelId = updatedRun.ModelId,
            PromptId = updatedRun.PromptId,
            ActualResponse = updatedRun.ActualResponse,
            Temp = updatedRun.Temp,
            Rating = updatedRun.Rating,
            UserRating = updatedRun.UserRating,    
        };
    }

    private async Task<RunDto> CreateRunAsync(Model model, Prompt prompt, ModelRunDto modelToRun, double temperature)
    {
        AiClientFactory factory = new AiClientFactory();
        IAiClient client = factory.GenerateClient(model);
        Stopwatch stopwatch = new Stopwatch();
        
        stopwatch.Start();
        string response = await client.GenerateResponseAsync(prompt.SystemMsg, prompt.UserMessage, temperature);
        stopwatch.Stop();
        
        var ts = stopwatch.ElapsedMilliseconds;
        var grade = await Grader.EvaluateRun(prompt.ExpectedResponse, response, ts);
        var run = await CreateRun(model.Id, prompt.Id, modelToRun, response, temperature, grade);

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

    public async Task<List<RunDto>> GetRunsForPromptAsync(int promptId)
    {
        var result = new List<RunDto>();
        var runs = await _runRepository.GetAllByPromptId(promptId);
        foreach (var run in runs)
        {
            result.Add(new RunDto
            {
                Id = run.Id,
                PromptId = run.PromptId,
                ModelId = run.ModelId,
                ActualResponse = run.ActualResponse,
                Temp = run.Temp,
                Rating = run.Rating,
                UserRating = run.UserRating,
            });
        }
        return result;
    }

    public async Task<List<RunDto>> GetRunsForModelAsync(int modelId)
    {
        var result = new List<RunDto>();
        var runs = await _runRepository.GetAllByModelId(modelId);
        foreach (var run in runs)
        {
            result.Add(new RunDto
            {
                Id = run.Id,
                PromptId = run.PromptId,
                ModelId = run.ModelId,
                ActualResponse = run.ActualResponse,
                Temp = run.Temp,
                Rating = run.Rating,
                UserRating = run.UserRating,
            });
        }
        return result;
    }
}