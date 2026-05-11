using MCQ3.DataConnect.Enums;

namespace MCQ3.DataConnect.Entities;

public class Exam : BaseEntity
{
    public Guid ChapterId { get; set; }
    public Chapter Chapter { get; set; } = null!;
    public string Title { get; set; } = string.Empty;
    public decimal TotalMarks { get; set; }
    public decimal PassingScore { get; set; }
    public int? TimeLimitSeconds { get; set; }
    public DateTime? AvailableFrom { get; set; }
    public DateTime? AvailableUntil { get; set; }
    public int? MaxAttempts { get; set; }
    public bool NegativeMarking { get; set; } = false;
    public bool ShuffleQuestions { get; set; } = false;
    public bool ShuffleOptions { get; set; } = false;
    public bool ShowAnswersAfter { get; set; } = false;
    public bool AutoReleaseResults { get; set; } = false;
    public ExamStatus Status { get; set; } = ExamStatus.Draft;

    public ICollection<ExamQuestion> ExamQuestions { get; set; } = new List<ExamQuestion>();
    public ICollection<Attempt> Attempts { get; set; } = new List<Attempt>();
}

public class ExamQuestion : BaseEntity
{
    public Guid ExamId { get; set; }
    public Exam Exam { get; set; } = null!;
    public Guid QuestionId { get; set; }
    public Question Question { get; set; } = null!;
    public int OrderIndex { get; set; }
}