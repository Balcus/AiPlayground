namespace AiPlayground.DataAccess.Entities;

public class Run
{
    public int Id { get; set; }
    public string ActualResponse { get; set; } = string.Empty;
    public double Rating { get; set; }
    public double UserRating { get; set; }
    public double Temp { get; set; }
    
    public int PromptId { get; set; }
    public int ModelId { get; set; }

    public virtual Prompt Prompt { get; set; } = null!;
    public virtual Model Model { get; set; } = null!;

}