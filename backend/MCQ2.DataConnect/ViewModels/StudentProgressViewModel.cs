namespace MCQ3.DataConnect.ViewModels;

public record StudentProgressViewModel(
    Guid StudentId,
    string StudentName,
    string StudentEmail,
    int TotalAttempts,
    decimal BestScore,
    decimal AverageScore,
    DateTime? LastAttemptAt
);