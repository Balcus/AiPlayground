namespace AiPlayground.BusinessLogic.AiClient.AiModels.Gemini;

public class GenerationConfig
{
    public List<string> stopSequences { get; set; } = [];
    public double temperature { get; set; }
    public int maxOutputTokens { get; set; }
    public double topP { get; set; }
    public int topK { get; set; }
}