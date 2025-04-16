namespace AiPlayground.BusinessLogic.Dtos;

public class PromptCreateDto
{
    public string UserMessage { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string SystemMsg { get; set; } = string.Empty;
    public string ExpectedResponse { get; set; } = string.Empty;
    public int ScopeId { get; set; }
}