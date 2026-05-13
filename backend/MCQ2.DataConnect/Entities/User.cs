namespace MCQ3.DataConnect.Entities;

public class UserAccount : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public bool IsActive { get; set; } = false;
    public bool TempPassword { get; set; } = false;
    public Guid? CreatedById { get; set; }

    public Guid? RoleId { get; set; }
    public Role? RoleEntity { get; set; }

    public Teacher? Teacher { get; set; }
    public Student? Student { get; set; }

    public UserAccount? CreatedBy { get; set; }
    public ICollection<Subject> Subjects { get; set; } = new List<Subject>();
    public ICollection<Enrolment> Enrolments { get; set; } = new List<Enrolment>();
    public ICollection<Attempt> Attempts { get; set; } = new List<Attempt>();
    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    public ICollection<EmailVerification> EmailVerifications { get; set; } = new List<EmailVerification>();
    public ICollection<PasswordReset> PasswordResets { get; set; } = new List<PasswordReset>();
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}