namespace MCQ3.DataConnect.ViewModels;

public record QuestionAnalyticsViewModel(
    Guid QuestionId,
    string QuestionStem,
    int TotalAttempts,
    int CorrectCount,
    int IncorrectCount,
    int SkippedCount,
    decimal SuccessRate
);