namespace MCQ3.DataConnect.ViewModels;

public record ExamQuestionInfo(
    Guid Id,
    string StemText,
    List<AnswerOptionInfo> Options
);

public record AnswerOptionInfo(
    Guid Id,
    string OptionText
);

public record ExamInfo(
    Guid Id,
    string Title,
    int? TimeLimitSeconds,
    bool ShuffleQuestions,
    bool ShuffleOptions,
    List<ExamQuestionInfo> ExamQuestions
);

public record AttemptViewModel(
    Guid Id,
    Guid ExamId,
    string ExamTitle,
    Guid StudentId,
    string StudentName,
    int AttemptNumber,
    DateTime StartedAt,
    DateTime? SubmittedAt,
    int? TimeSpentSecs,
    decimal? Score,
    bool? IsPassed,
    bool IsReleased,
    bool AutoSubmitted,
    List<AttemptAnswerViewModel> Answers,
    ExamInfo? Exam = null
);