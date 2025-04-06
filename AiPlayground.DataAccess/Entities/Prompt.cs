namespace AiPlayground.DataAccess.Entities;

public class Prompt
{
    public int Id { get; set; }
    public string UserMessage { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string SystemMsg { get; set; } = string.Empty;
    public string ExpectedResponse { get; set; } = string.Empty;
    
    public int ScopeId;

    public virtual Scope Scope { get; set; } = null!;
    public virtual Run Run { get; set; } = null!;
}