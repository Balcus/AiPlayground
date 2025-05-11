using System.Text.Json;
using AiPlayground.BusinessLogic.AiClient.AiModels.Gemini;

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
    
    public async Task<string> GenerateResponseAsync(string systemMessage, string userMessage, double temperature)
    {
        var requestUri = $"https://generativelanguage.googleapis.com/v1beta/models/{_modelName}:generateContent?key={_apiKey}";
        var client = new HttpClient();
        var request = new GeminiRequest
        {
            system_instructions = new GeminiMessage()
            {
                parts = new List<GeminiMessagePart>
                {
                    new GeminiMessagePart { text = systemMessage }
                }
            },
            contents = new List<GeminiMessage>
            {
                new GeminiMessage
                {
                    parts = new List<GeminiMessagePart>
                    {
                        new GeminiMessagePart { text = userMessage }
                    }
                }
            },
            generationConfig = new GenerationConfig()
            {
                maxOutputTokens = 800,
                topP = 0.8,
                topK = 10,
                temperature = temperature
            }
        };
        var jsonContent = JsonSerializer.Serialize(request, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        });
        
        var httpContent = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

        try
        {
            var response = await client.PostAsync(requestUri, httpContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            var geminiResponse = JsonSerializer.Deserialize<GeminiResponse>(responseContent,
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                });
            return geminiResponse.candidates.First().content.parts.First().text;
        }
        catch (Exception e)
        {
            throw new Exception($"Exception thrown while generating gemini response: {e.Message}");
        }
    }
}