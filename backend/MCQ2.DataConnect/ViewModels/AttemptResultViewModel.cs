namespace MCQ3.DataConnect.ViewModels;

public record AttemptResultViewModel(
    Guid AttemptId,
    decimal Score,
    bool IsPassed,
    bool IsReleased,
    int TotalQuestions,
    int CorrectCount,
    int IncorrectCount,
    int SkippedCount,
    List<QuestionResult> QuestionResults
);