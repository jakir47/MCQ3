namespace MCQ3.DataConnect.ViewModels;

public record AnswerOptionViewModel(
    Guid Id,
    string? OptionText,
    string? ImagePath,
    string? AudioPath,
    bool IsCorrect,
    int OrderIndex
);