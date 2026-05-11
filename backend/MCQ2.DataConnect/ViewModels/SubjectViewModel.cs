namespace MCQ3.DataConnect.ViewModels;

public record SubjectViewModel(
    Guid Id,
    string Title,
    string? Description,
    bool IsArchived,
    Guid TeacherId,
    string TeacherName,
    DateTime CreatedAt,
    int ChapterCount,
    int QuestionCount
);