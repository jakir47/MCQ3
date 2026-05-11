namespace MCQ3.DataConnect.ViewModels;

public record ExamReviewViewModel(
    Guid AttemptId,
    string ExamTitle,
    string SubjectName,
    decimal Score,
    bool IsPassed,
    int TotalQuestions,
    int CorrectCount,
    int IncorrectCount,
    int SkippedCount,
    List<QuestionReviewItem> QuestionReviews
);

public record QuestionReviewItem(
    Guid QuestionId,
    string QuestionText,
    List<OptionReviewItem> Options,
    Guid? SelectedOptionId,
    string? SelectedOptionText,
    Guid? CorrectOptionId,
    string? CorrectOptionText,
    bool IsCorrect,
    decimal MarksAwarded,
    string? Explanation
);

public record OptionReviewItem(
    Guid Id,
    string OptionText,
    bool IsCorrect,
    bool IsSelected
);