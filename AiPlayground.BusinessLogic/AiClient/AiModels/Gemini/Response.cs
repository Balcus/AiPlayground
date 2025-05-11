namespace AiPlayground.BusinessLogic.AiClient.AiModels.Gemini;

public class Response
{
    public List<Candidate> candidates { get; set; } = [];
    public UsageMetadata usageMetadata { get; set; }
    public string modelVersion { get; set; } = string.Empty;
}