namespace AiPlayground.BusinessLogic.AiClient.AiModels.Gemini;

public class Request
{
    public SystemInstruction system_instruction { get; set; }
    public List<Content> contents { get; set; } = [];
    public GenerationConfig generationConfig { get; set; }
}