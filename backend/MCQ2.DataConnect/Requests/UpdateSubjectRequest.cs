namespace MCQ3.DataConnect.Requests;

public class UpdateSubjectRequest
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool? IsArchived { get; set; }
}