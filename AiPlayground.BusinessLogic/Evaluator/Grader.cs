namespace AiPlayground.BusinessLogic.Evaluator;

public class Grader
{
    private const double CosineWeight = 0.3;
    private const double WaitTimeWeight = 0.7;

    private const long GreatTime = 3000;
    private const long GoodTime = 5000;
    private const long AcceptableTime = 10000;
    private const long BadTime = 20000;

    public async Task<double> EvaluateRun(string expectedRespone, string aiResponse, long timeSpan)
    {
        double embeddingScore = await EmbeddingEvaluator.GetEmbeddingScore(expectedRespone, aiResponse);
        double timeScore = CalculateTimeScore(timeSpan);
        
        double grade = CosineWeight * embeddingScore + WaitTimeWeight * timeScore;
        return Math.Clamp(grade, 0, 10);
    }

    public double CalculateTimeScore(long timeSpan)
    {
        switch (timeSpan)
        {
            case <= GreatTime:
                return 10;
            case <= GoodTime:
                return 8;
            case <= AcceptableTime:
                return 6;
            case <= BadTime:
                return 3;
            default:
                return 1;
        }
    }
}