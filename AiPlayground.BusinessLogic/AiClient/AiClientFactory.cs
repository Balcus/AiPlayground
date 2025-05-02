using AiPlayground.BusinessLogic.Enums;
using AiPlayground.DataAccess.Entities;

namespace AiPlayground.BusinessLogic.AiClient;

public class AiClientFactory : IAiClientFactory
{
    public IAiClient GenerateClient(Model model)
    {
        return (PlatformType)model.PlatformId switch
        {
            PlatformType.OpenAi => new OpenAiClient(
                model.Name, 
                Environment.GetEnvironmentVariable("OPENAI_API_KEY") ?? throw new InvalidOperationException()),
            
            PlatformType.DeepSeek => new DeepSeekClient(
                model.Name, 
                Environment.GetEnvironmentVariable("DEEPSEEK_API_KEY") ?? throw new InvalidOperationException()),
            
            PlatformType.Gemini => new GeminiClient(
                model.Name, 
                Environment.GetEnvironmentVariable("GEMINI_API_KEY") ?? throw new InvalidOperationException()),
            
            _ => throw new ArgumentException($"Unsupported platform : {model.PlatformId}")
        };
    }
}