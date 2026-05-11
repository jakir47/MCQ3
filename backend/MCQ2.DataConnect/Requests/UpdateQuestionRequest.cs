namespace MCQ3.DataConnect.Requests;

public class UpdateQuestionRequest
{
    public string? StemText { get; set; }
    public string? StemImagePath { get; set; }
    public string? StemAudioPath { get; set; }
    public string? StemVideoUrl { get; set; }
    public string? Explanation { get; set; }
    public decimal? PositiveMarks { get; set; }
    public decimal? NegativeMarks { get; set; }
    public string? Difficulty { get; set; }
    public string? Tags { get; set; }
    public List<UpdateAnswerOptionRequest>? Options { get; set; }
}