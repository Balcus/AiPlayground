using AiPlayground.BusinessLogic.Enums;
using AiPlayground.DataAccess.Entities;

namespace AiPlayground.BusinessLogic.AiClient;

// TODO: DO NOT PUSH THIS TO GITHUB
// set the keys as env variables
// create a rating model for the messgaes
public class AiClientFactory : IAiClientFactory
{
    public IAiClient GenerateClient(Model model)
    {
        return (PlatformType)model.PlatformId switch
        {
            PlatformType.OpenAi => new OpenAiClient(
                model.Name,
                Environment.GetEnvironmentVariable("OPENAI_API_KEY") ?? throw new Exception("OPENAI_API_KEY is missing")
                ),
            
            PlatformType.DeepSeek => new DeepSeekClient(
                model.Name, 
                Environment.GetEnvironmentVariable("DEEPSEEK_API_KEY") ?? throw new Exception("DEEPSEEK_API_KEY is missing")
                ),
            
            PlatformType.Gemini => new GeminiClient(
                model.Name, 
                Environment.GetEnvironmentVariable("GEMINI_API_KEY") ?? throw new Exception("GEMINI_API_KEY is missing")
                ),
            
            _ => throw new ArgumentException($"Unsupported platform : {model.PlatformId}")
        };
    }
}