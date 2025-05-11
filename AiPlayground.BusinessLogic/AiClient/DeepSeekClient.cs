using System.Net.Http.Headers;
using System.Text.Json;
using AiPlayground.BusinessLogic.AiClient.AiModels;

namespace AiPlayground.BusinessLogic.AiClient;

public class DeepSeekClient : IAiClient
{
    private readonly string _modelName;
    private readonly string _apiKey;

    public DeepSeekClient(string modelName, string apiKey)
    {
        _modelName = modelName;
        _apiKey = apiKey;
    }
    
    public async Task<string> GenerateResponseAsync(string systemMessage, string userMessage, double temperature)
    {
        var requestUri = "https://api.deepseek.com/chat/completions";
        var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        var content = new DeepSeekRequest
        {
            Model = _modelName,
            Messages =
            {
                new DeepSeekMessage
                {
                    Role = "system",
                    Content = systemMessage,
                },
                new DeepSeekMessage
                {
                    Role = "user",
                    Content = userMessage,
                }
            },
            Stream = false,
        };
        
        var jsonContent = JsonSerializer.Serialize(content, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        });
        
        var httpContent = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

        try
        {
            var response = await client.PostAsync(requestUri, httpContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            var deepSeekResponse = JsonSerializer.Deserialize<DeepSeekResponse>(responseContent,
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                });
            
            return deepSeekResponse.Choices.First().Message.Content;
        }
        catch (Exception e)
        {
            throw new Exception($"Exception thrown while generating deepseek response: {e.Message}");
        }
        
    }
}