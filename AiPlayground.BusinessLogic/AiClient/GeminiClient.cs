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
        var request = new Request
        {
            system_instruction = new SystemInstruction
            {
                parts = new List<Part>
                {
                    new Part() { text = systemMessage }
                }
            },

            contents = new List<Content>
            {
                new Content()
                {
                    parts = new List<Part>
                    {
                        new Part() { text = userMessage }
                    }
                }
            },

            generationConfig = new GenerationConfig
            {
                stopSequences = new List<string>
                {
                    "Title"
                },
                temperature = temperature,
                maxOutputTokens = 800,
                topP = 0.8,
                topK = 10
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
            var geminiResponse = JsonSerializer.Deserialize<Response>(responseContent,
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