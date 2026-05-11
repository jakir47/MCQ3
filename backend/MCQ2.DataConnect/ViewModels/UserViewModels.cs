namespace MCQ3.DataConnect.ViewModels;

public record TeacherViewModel(
    Guid Id,
    string FullName,
    string Email,
    bool IsActive,
    int SubjectCount,
    DateTime CreatedAt
);

public record StudentViewModel(
    Guid Id,
    string FullName,
    string Email,
    bool IsActive
);

public record StudentChapterViewModel(
    Guid Id,
    Guid StudentId,
    string StudentName,
    string StudentEmail,
    Guid? AssignedById,
    DateTime CreatedAt
);