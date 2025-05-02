namespace AiPlayground.BusinessLogic.AiClient;

public class GeminiClient : IAiClient
{
    private readonly string _modelName;
    private readonly string _apiKey;

    public GeminiClient(string modelName, string apiKey)
    {
        _modelName = modelName;
        _apiKey = apiKey;
    }
    
    public Task<string> GenerateResponseAsync(string systemMessage, string userMessage, double temperature)
    {
        throw new NotImplementedException();
    }
}