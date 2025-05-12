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
    private readonly IRepository<Run> _runRepository;
    private readonly IRepository<Model> _modelRepository;
    private readonly IRepository<Prompt> _promptRepository;
    private readonly Grader _runGrade;
    
    public RunService(IRepository<Run> runRepository, IRepository<Model> modelRepository, IRepository<Prompt> promptRepository)
    {
        _runRepository = runRepository;
        _modelRepository = modelRepository;
        _promptRepository = promptRepository;
        _runGrade = new Grader();
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
            
            var run = await CreateRunAsync(model, prompt, modelToRun, modelToRun.Temperature);
            runs.Add(run);
            
        }
        return runs;
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
        
        /* THE GRADE WILL TAKE INTO ACCOUNT :
            - Cosine Similarity between the embeddings of the 2 responses
            - How much time it took for the AI to give an answer
            - If it respected the system message
            - And finally the user score which will be the most important metric
        */
        var grade = await _runGrade.EvaluateRun(prompt.ExpectedResponse, response, ts);
        var run = await CreateRun(model.Id, prompt.Id, modelToRun, response, (double)temperature, grade);

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