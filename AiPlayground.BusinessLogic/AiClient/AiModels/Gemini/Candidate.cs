namespace AiPlayground.BusinessLogic.AiClient.AiModels.Gemini;

public class Candidate
{
    public Content content { get; set; }
    public string finishReason { get; set; }
    public double avgLogprobs { get; set; }
}