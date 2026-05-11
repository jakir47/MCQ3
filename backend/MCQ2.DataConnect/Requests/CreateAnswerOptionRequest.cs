namespace MCQ3.DataConnect.Requests;

public class CreateAnswerOptionRequest
{
    public string? OptionText { get; set; }
    public string? ImagePath { get; set; }
    public string? AudioPath { get; set; }
    public bool IsCorrect { get; set; }
}