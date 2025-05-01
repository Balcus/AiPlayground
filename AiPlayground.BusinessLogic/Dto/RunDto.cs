namespace AiPlayground.BusinessLogic.Dto;

public class RunDto
{
    public int Id { get; set; }
    public int PromptId { get; set; }
    public int ModelId { get; set; }
    public string ActualResponse { get; set; } = string.Empty;
    public double Rating { get; set; }
    public double UserRating { get; set; }
    public double Temp { get; set; }
}