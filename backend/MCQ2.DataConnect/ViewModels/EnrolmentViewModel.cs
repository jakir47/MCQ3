namespace MCQ3.DataConnect.ViewModels;

public record EnrolmentViewModel(
    Guid Id,
    Guid StudentId,
    string StudentName,
    string StudentEmail,
    Guid ChapterId,
    string ChapterTitle,
    Guid? ExamId,
    string? ExamTitle,
    Guid EnrolledById,
    string EnrolledByName,
    DateTime EnrolledAt,
    DateTime? ExpiresAt,
    DateTime? RemovedAt
);