namespace MCQ3.DataConnect.ViewModels;

public record ExamSummary(
    Guid ExamId,
    string Title,
    int TimeLimitSeconds,
    bool IsAvailable,
    int? MaxAttempts,
    int AttemptsCount
);