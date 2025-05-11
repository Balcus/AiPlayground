namespace AiPlayground.BusinessLogic.AiClient.AiModels.Gemini;

public class GeminiRequest
{
    public List<GeminiContent> Contents { get; set; } = [];
    public GenerationConfig GenerationConfig { get; set; }
    public SystemInstruction SystemInstruction { get; set; }
}