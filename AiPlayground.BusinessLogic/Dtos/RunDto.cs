namespace AiPlayground.BusinessLogic.Dtos;

public class RunDto
{
    public int Id { get; set; }
    public string ActualResponse { get; set; } = string.Empty;
    public decimal Rating { get; set; }
    public decimal UserRating { get; set; }
    public decimal Temp { get; set; }
    
    public int PromptId { get; set; }
    public int ModelId { get; set; }
}