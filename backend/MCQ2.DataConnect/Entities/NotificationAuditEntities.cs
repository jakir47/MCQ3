namespace MCQ3.DataConnect.Entities;

public class Notification : BaseEntity
{
    public Guid UserId { get; set; }
    public UserAccount User { get; set; } = null!;
    public string Type { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public bool IsRead { get; set; } = false;
    public Guid? EntityId { get; set; }
}

public class AuditLog : BaseEntity
{
    public Guid ActorId { get; set; }
    public UserAccount Actor { get; set; } = null!;
    public string Action { get; set; } = string.Empty;
    public string TargetType { get; set; } = string.Empty;
    public Guid TargetId { get; set; }
    public string? Metadata { get; set; }
}