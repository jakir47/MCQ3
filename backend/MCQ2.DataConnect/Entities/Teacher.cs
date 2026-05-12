namespace MCQ3.DataConnect.Entities;

public class Teacher
{
    public Guid Id => UserId;

    public string Name { get; set; } = string.Empty;
    public string FullName => Name;
    public string Email { get; set; } = string.Empty;
    public string NID { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string ContactNo { get; set; } = string.Empty;
    public string? PhotoUrl { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public Guid UserId { get; set; }
    public UserAccount User { get; set; } = null!;

    public ICollection<Subject> Subjects { get; set; } = new List<Subject>();
}