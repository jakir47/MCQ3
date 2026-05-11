namespace MCQ3.DataConnect.ViewModels;

public record QuestionResult(
    Guid QuestionId,
    string Stem,
    Guid? SelectedOptionId,
    string? SelectedOptionText,
    string? CorrectOptionText,
    bool? IsCorrect,
    decimal MarksAwarded,
    string? Explanation
);