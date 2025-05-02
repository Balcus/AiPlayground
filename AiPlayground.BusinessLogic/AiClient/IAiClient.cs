namespace AiPlayground.BusinessLogic.AiClient;

public interface IAiClient
{
    Task<string> GenerateResponseAsync(string systemMessage, string userMessage, double temperature);
}