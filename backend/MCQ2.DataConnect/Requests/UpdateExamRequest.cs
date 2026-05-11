namespace MCQ3.DataConnect.Requests;

public class UpdateExamRequest
{
    public string? Title { get; set; }
    public decimal? TotalMarks { get; set; }
    public decimal? PassingScore { get; set; }
    public int? TimeLimitSeconds { get; set; }
    public DateTime? AvailableFrom { get; set; }
    public DateTime? AvailableUntil { get; set; }
    public int? MaxAttempts { get; set; }
    public bool? NegativeMarking { get; set; }
    public bool? ShuffleQuestions { get; set; }
    public bool? ShuffleOptions { get; set; }
    public bool? ShowAnswersAfter { get; set; }
    public bool? AutoReleaseResults { get; set; }
    public List<Guid>? QuestionIds { get; set; }
}