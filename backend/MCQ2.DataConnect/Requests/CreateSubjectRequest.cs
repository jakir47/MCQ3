namespace MCQ3.DataConnect.Requests;

public class CreateSubjectRequest
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
}