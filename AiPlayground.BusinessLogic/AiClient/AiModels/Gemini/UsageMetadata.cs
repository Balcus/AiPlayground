namespace AiPlayground.BusinessLogic.AiClient.AiModels.Gemini;

public class UsageMetadata
{
    public int promptTokenCount { get; set; }
    public int candidatesTokenCount { get; set; }
    public int totalTokenCount { get; set; }
    public List<TokenDetail> promptTokensDetails { get; set; } = [];
    public List<TokenDetail> candidatesTokensDetails { get; set; } = [];
}