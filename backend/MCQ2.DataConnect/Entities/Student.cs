namespace MCQ3.DataConnect.Entities;

public class Student
{
    public Guid Id => UserId ?? Guid.Empty;

    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string FullName => Name;
    public string NID { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string ContactNo { get; set; } = string.Empty;
    public string FatherName { get; set; } = string.Empty;
    public string FatherContact { get; set; } = string.Empty;
    public string MotherName { get; set; } = string.Empty;
    public string MotherContact { get; set; } = string.Empty;
    public string? PhotoUrl { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public Guid? UserId { get; set; }
    public UserAccount? User { get; set; }

    public ICollection<Enrolment> Enrolments { get; set; } = new List<Enrolment>();
    public ICollection<Attempt> Attempts { get; set; } = new List<Attempt>();
    public ICollection<StudentChapter> StudentChapters { get; set; } = new List<StudentChapter>();
}