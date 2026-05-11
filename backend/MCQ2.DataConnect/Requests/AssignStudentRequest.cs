namespace MCQ3.DataConnect.Requests;

public class AssignStudentRequest
{
    public Guid StudentId { get; set; }
    public Guid ChapterId { get; set; }
}