namespace MCQ3.DataConnect.ViewModels;

public record TrendViewModel(
    DateTime Date,
    int Attempts,
    decimal AverageScore
);