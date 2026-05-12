namespace MCQ3.DataConnect.Entities;

public class Enrolment : BaseEntity
{
    public Guid? StudentId { get; set; }
    public Student? Student { get; set; }
    public Guid ChapterId { get; set; }
    public Chapter Chapter { get; set; } = null!;
    public Guid? ExamId { get; set; }
    public Exam? Exam { get; set; }
    public Guid EnrolledById { get; set; }
    public UserAccount EnrolledBy { get; set; } = null!;
    public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
    public DateTime? ExpiresAt { get; set; }
    public DateTime? RemovedAt { get; set; }
    public Guid? TransferredFromChapterId { get; set; }
}