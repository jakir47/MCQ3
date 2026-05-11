namespace MCQ3.DataConnect.Requests;

public class CreateChapterRequest
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int OrderIndex { get; set; }
}