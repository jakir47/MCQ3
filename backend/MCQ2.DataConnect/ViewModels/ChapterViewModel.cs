namespace MCQ3.DataConnect.ViewModels;

public record ChapterViewModel(
    Guid Id,
    string Title,
    string? Description,
    int OrderIndex,
    bool IsArchived,
    Guid SubjectId,
    DateTime CreatedAt,
    int QuestionCount,
    int ExamCount,
    int StudentCount
);