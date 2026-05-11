namespace MCQ3.DataConnect.Requests;

public class UpdateChapterRequest
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int? OrderIndex { get; set; }
    public bool? IsArchived { get; set; }
}