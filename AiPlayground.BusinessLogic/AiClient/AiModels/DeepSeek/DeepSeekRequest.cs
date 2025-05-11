namespace AiPlayground.BusinessLogic.AiClient.AiModels;

public class DeepSeekRequest
{
    public string Model { get; set; } = string.Empty;
    public List<DeepSeekMessage> Messages { get; set; } = [];
    public bool Stream { get; set; } = false;
}