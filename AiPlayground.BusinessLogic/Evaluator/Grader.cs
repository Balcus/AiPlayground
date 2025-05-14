using Porter2Stemmer;

namespace AiPlayground.BusinessLogic.Evaluator;

public class Grader
{
    private const double CosineWeight = 0.3;
    private const double WaitTimeWeight = 0.4;
    private const double OverlapWeight = 0.3;
    public static async Task<double> EvaluateRun(string expectedResponse, string actualResponse, long timeSpan)
    {
        float embeddingScore = await EmbeddingEvaluator.GetEmbeddingScore(expectedResponse, actualResponse);
        double overlapScore = GetOverlapScore(expectedResponse, actualResponse);
        double timeScore = CalculateTimeScore(timeSpan);
        
        double grade = CosineWeight * embeddingScore + WaitTimeWeight * timeScore + OverlapWeight * overlapScore;
        Console.WriteLine($"emb: {embeddingScore}, ovlp: {overlapScore}, tm: {timeScore}");
        return Math.Clamp(grade, 0, 10);
    }
    private static HashSet<string> Preprocess(string str)
    {
        char[] separators =
        [
            ' ', '\t', '\n', '\r', '\v', '\f',
            ',', '.', ';', '!', '?', ':', '(', ')', '"', '\'', '-', '_', '/', '\\', '|', 
            '<', '>', '[', ']', '{', '}', '*', '#'
        ];
        
        HashSet<string> stopWords = new HashSet<string>
        {
            "i", "me", "my", "myself", "we", "our", "ours", "ourselves", "you", "your", "yours", "yourself", "yourselves",
            "he", "him", "his", "himself", "she", "her", "hers", "herself", "it", "its", "itself", "they", "them", "their",
            "theirs", "themselves", "what", "which", "who", "whom", "this", "that", "these", "those", "am", "is", "are", "was",
            "were", "be", "been", "being", "have", "has", "had", "having", "do", "does", "did", "doing", "a", "an", "the", "and",
            "but", "if", "or", "because", "as", "until", "while", "of", "at", "by", "for", "with", "about", "against", "between",
            "into", "through", "during", "before", "after", "above", "below", "to", "from", "up", "down", "in", "out", "on", "off",
            "over", "under", "again", "further", "then", "once", "here", "there", "when", "where", "why", "how", "all", "any", "both",
            "each", "few", "more", "most", "other", "some", "such", "no", "nor", "not", "only", "own", "same", "so", "than", "too",
            "very", "s", "t", "can", "will", "just", "don", "should", "now"
        };
        
        string[] words = str.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        HashSet<string> wordSet = new HashSet<string>();
        EnglishPorter2Stemmer stemmer = new EnglishPorter2Stemmer();
        foreach (string word in words)
        {
            string lowerCaseWord = word.ToLower();
            if (!stopWords.Contains(lowerCaseWord))
            {
                string stemmedWord = stemmer.Stem(lowerCaseWord).Value;
                wordSet.Add(stemmedWord);
            }
        }
        return wordSet;
    }
    
    private static double GetOverlapScore(string expected, string actual)
    {
        var expectedSet = Preprocess(expected);
        var actualSet = Preprocess(actual);

        if (expectedSet.Count == 0)
            return 0;

        int matches = expectedSet.Intersect(actualSet).Count();
        double overlapPercentage = (double)matches / expectedSet.Count;
        return overlapPercentage * 10;
    }
    
    private static double CalculateTimeScore(long timeSpan)
    {
        const double maxScore = 10.0;
        const double minScore = 0.0;
        const double maxTime = 20_000.0;

        if (timeSpan <= 0)
            return maxScore;

        double score = maxScore - (timeSpan / maxTime) * (maxScore - minScore);
        return Math.Clamp(score, minScore, maxScore);
    }

}