namespace MCQ3.DataConnect.Entities;

public class StudentChapter : BaseEntity
{
    public Guid? StudentId { get; set; }
    public Student? Student { get; set; }

    public Guid ChapterId { get; set; }
    public Chapter Chapter { get; set; } = null!;

    public bool IsActive { get; set; } = true;

    public Guid? AssignedById { get; set; }
    public UserAccount? AssignedBy { get; set; }
}