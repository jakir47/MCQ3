namespace MCQ3.DataConnect.ViewModels;

public record StudentExamViewModel(
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
    Guid ChapterId,
    string? ChapterTitle,
    string? SubjectName,
    DateTime CreatedAt,
    int QuestionCount,
    int MyAttempts,
    int MySubmittedAttempts,
    bool HasInProgressAttempt,
    bool IsPassed,
    bool IsResultsReleased,
    decimal? BestScore,
    DateTime? LastAttemptAt
);