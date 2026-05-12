namespace MCQ3.DataConnect.ViewModels;

public record TeacherViewModel(
    Guid Id,
    string FullName,
    string Email,
    bool IsActive,
    int SubjectCount,
    DateTime CreatedAt,
    string? Title = null,
    string? Phone = null,
    string? Address = null,
    string? NID = null
);

public record StudentViewModel(
    Guid Id,
    string FullName,
    string Email,
    bool IsActive,
    DateTime CreatedAt,
    string? Code = null,
    string? NID = null,
    string? Address = null,
    string? Phone = null,
    string? FatherName = null,
    string? FatherContact = null,
    string? MotherName = null,
    string? MotherContact = null,
    string? Username = null
);

public record StudentChapterViewModel(
    Guid Id,
    Guid StudentId,
    string StudentName,
    string StudentEmail,
    Guid? AssignedById,
    DateTime CreatedAt
);