using OpenAI.Chat;

namespace AiPlayground.BusinessLogic.AiClient;

public class OpenAiClient : IAiClient
{
    private readonly string _modelName;
    private readonly string _apiKey;

    public OpenAiClient(string modelName, string apiKey)
    {
        _modelName = modelName;
        _apiKey = apiKey;
    }
    
    public async Task<string> GenerateResponseAsync(string systemMessage, string userMessage, double temperature)
    {
        ChatClient chatClient = new ChatClient(_modelName, _apiKey);
        
        var systemMsg = new SystemChatMessage(systemMessage);
        var userMsg = new UserChatMessage(userMessage);

        var message = new List<ChatMessage>
        {
            systemMsg,
            userMsg
        };

        var options = new ChatCompletionOptions
        {
            Temperature = (float)temperature,
        };
        
        ChatCompletion response = await chatClient.CompleteChatAsync(message, options);
        return response.Content.First().Text;
    }
}