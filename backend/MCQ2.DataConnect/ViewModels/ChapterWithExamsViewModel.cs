namespace MCQ3.DataConnect.ViewModels;

public record ChapterWithExamsViewModel(
    Guid ChapterId,
    string ChapterTitle,
    Guid SubjectId,
    string SubjectTitle,
    List<ExamSummary> Exams
);