namespace MCQ3.DataConnect.ViewModels;

public record AuditLogViewModel(
    Guid Id,
    Guid ActorId,
    string ActorName,
    string Action,
    string TargetType,
    Guid TargetId,
    string? Metadata,
    DateTime CreatedAt
);