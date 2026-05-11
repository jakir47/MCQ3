using MCQ3.DataConnect.Enums;

namespace MCQ3.DataConnect.ViewModels;

public record ExamViewModel(
    Guid Id,
    string Title,
    decimal TotalMarks,
    decimal PassingScore,
    int? TimeLimitSeconds,
    DateTime? AvailableFrom,
    DateTime? AvailableUntil,
    int? MaxAttempts,
    bool NegativeMarking,
    bool ShuffleQuestions,
    bool ShuffleOptions,
    bool ShowAnswersAfter,
    bool AutoReleaseResults,
    ExamStatus Status,
    Guid ChapterId,
    string? ChapterTitle,
    string? SubjectName,
    DateTime CreatedAt,
    int QuestionCount,
    int AttemptCount,
    List<ExamQuestionViewModel>? ExamQuestions
);

public record ExamQuestionViewModel(
    Guid QuestionId,
    string? StemText,
    string? Difficulty
);