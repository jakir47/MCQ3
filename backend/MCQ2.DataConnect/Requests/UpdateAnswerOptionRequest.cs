namespace MCQ3.DataConnect.Requests;

public class UpdateAnswerOptionRequest
{
    public Guid Id { get; set; }
    public string? OptionText { get; set; }
    public string? ImagePath { get; set; }
    public string? AudioPath { get; set; }
    public bool? IsCorrect { get; set; }
}