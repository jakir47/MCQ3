namespace MCQ3.DataConnect.ViewModels;

public record ExamSummaryViewModel(
    Guid ExamId,
    string ExamTitle,
    int TotalStudents,
    int TotalAttempts,
    int PassedCount,
    int FailedCount,
    decimal AverageScore,
    decimal HighestScore,
    decimal LowestScore
);