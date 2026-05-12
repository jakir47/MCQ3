namespace MCQ3.DataConnect.Entities;

public class Attempt : BaseEntity
{
    public Guid ExamId { get; set; }
    public Exam Exam { get; set; } = null!;
    public Guid? StudentId { get; set; }
    public Student? Student { get; set; }
    public int AttemptNumber { get; set; }
    public DateTime StartedAt { get; set; } = DateTime.UtcNow;
    public DateTime? SubmittedAt { get; set; }
    public int? TimeSpentSecs { get; set; }
    public decimal? Score { get; set; }
    public bool? IsPassed { get; set; }
    public bool IsReleased { get; set; } = false;
    public bool AutoSubmitted { get; set; } = false;
    public string? ResumeData { get; set; }

    public ICollection<AttemptAnswer> AttemptAnswers { get; set; } = new List<AttemptAnswer>();
}

public class AttemptAnswer : BaseEntity
{
    public Guid AttemptId { get; set; }
    public Attempt Attempt { get; set; } = null!;
    public Guid QuestionId { get; set; }
    public Question Question { get; set; } = null!;
    public Guid? SelectedOptionId { get; set; }
    public AnswerOption? SelectedOption { get; set; }
    public bool? IsCorrect { get; set; }
    public decimal? MarksAwarded { get; set; }
}