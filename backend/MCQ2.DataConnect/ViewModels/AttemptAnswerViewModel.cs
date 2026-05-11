namespace MCQ3.DataConnect.ViewModels;

public record AttemptAnswerViewModel(
    Guid Id,
    Guid QuestionId,
    string QuestionStem,
    Guid? SelectedOptionId,
    string? SelectedOptionText,
    bool? IsCorrect,
    decimal? MarksAwarded
);