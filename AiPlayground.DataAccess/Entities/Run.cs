namespace AiPlayground.DataAccess.Entities;

public class Run
{
    public int Id { get; set; }
    public string ActualResponse { get; set; } = string.Empty;
    public decimal Rating { get; set; }
    public decimal UserRating { get; set; }
    public decimal Temp { get; set; }
    
    public int PromptId { get; set; }
    public int ModelId { get; set; }

    public virtual Prompt Prompt { get; set; } = null!;
    public virtual Model Model { get; set; } = null!;

}