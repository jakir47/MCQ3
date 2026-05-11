using MCQ3.DataConnect.Enums;

namespace MCQ3.DataConnect.ViewModels;

public record QuestionViewModel(
    Guid Id,
    string? StemText,
    string? StemImagePath,
    string? StemAudioPath,
    string? StemVideoUrl,
    string? Explanation,
    decimal PositiveMarks,
    decimal NegativeMarks,
    Difficulty? Difficulty,
    string Tags,
    Guid ChapterId,
    DateTime CreatedAt,
    List<AnswerOptionViewModel> Options
);