namespace MCQ3.DataConnect.Entities;

public class Subject : BaseEntity
{
    public Guid TeacherId { get; set; }
    public Teacher Teacher { get; set; } = null!;
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsArchived { get; set; } = false;

    public ICollection<Chapter> Chapters { get; set; } = new List<Chapter>();
}

public class Chapter : BaseEntity
{
    public Guid SubjectId { get; set; }
    public Subject Subject { get; set; } = null!;
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int OrderIndex { get; set; } = 0;
    public bool IsArchived { get; set; } = false;

    public ICollection<Question> Questions { get; set; } = new List<Question>();
    public ICollection<Exam> Exams { get; set; } = new List<Exam>();
    public ICollection<Enrolment> Enrolments { get; set; } = new List<Enrolment>();
}