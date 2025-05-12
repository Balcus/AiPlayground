using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace AiPlayground.BusinessLogic.Evaluator;

public class EmbeddingEvaluator
{
    private static readonly HttpClient Client = new HttpClient();
    private static readonly string ApiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY") ?? throw new Exception("OPENAI_API_KEY is required.");

    public static async Task<float> GetEmbeddingScore(string expectedResponse, string actualResponse)
    {
        var expectedVec = await GetEmbeddingAsync(expectedResponse);
        var actualVec = await GetEmbeddingAsync(actualResponse);

        float similarity = CosineSimilarity(expectedVec, actualVec);
        float grade = similarity * 9 + 1;
        return similarity;
    }

    public static async Task<List<float>> GetEmbeddingAsync(string input)
    {
        var requestUri = "https://api.openai.com/v1/embeddings";
        var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);

        var embeddingRequest = new
        {
            input = input,
            model = "text-embedding-3-small"
        };

        var jsonContent = JsonSerializer.Serialize(embeddingRequest, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        });

        var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        try
        {
            var response = await client.PostAsync(requestUri, httpContent);
            var responseContent = await response.Content.ReadAsStringAsync();

            var embeddingResponse = JsonDocument.Parse(responseContent);
            var embeddingArray = embeddingResponse.RootElement
                .GetProperty("data")[0]
                .GetProperty("embedding");

            var embeddingList = embeddingArray.EnumerateArray()
                .Select(x => x.GetSingle())
                .ToList();

            return embeddingList;
        }
        catch (Exception e)
        {
            throw new Exception($"Exception thrown while fetching embedding: {e.Message}");
        }
    }


    private static float CosineSimilarity(List<float> v1, List<float> v2)
    {
        float dot = 0f, mag1 = 0f, mag2 = 0f;

        for (int i = 0; i < v1.Count; i++)
        {
            dot += v1[i] * v2[i];
            mag1 += v1[i] * v1[i];
            mag2 += v2[i] * v2[i];
        }

        return dot / ((float)Math.Sqrt(mag1) * (float)Math.Sqrt(mag2));
    }
}