using MCQ3.DataConnect.Enums;

namespace MCQ3.DataConnect.Entities;

public class Question : BaseEntity
{
    public Guid ChapterId { get; set; }
    public Chapter Chapter { get; set; } = null!;
    public Guid? SourceQuestionId { get; set; }
    public Question? SourceQuestion { get; set; }

    public string? StemText { get; set; }
    public string? StemImagePath { get; set; }
    public string? StemAudioPath { get; set; }
    public string? StemVideoUrl { get; set; }

    public string? Explanation { get; set; }
    public decimal PositiveMarks { get; set; }
    public decimal NegativeMarks { get; set; } = 0;
    public Difficulty? Difficulty { get; set; }
    public string Tags { get; set; } = "[]";

    public ICollection<AnswerOption> AnswerOptions { get; set; } = new List<AnswerOption>();
    public ICollection<ExamQuestion> ExamQuestions { get; set; } = new List<ExamQuestion>();
    public ICollection<AttemptAnswer> AttemptAnswers { get; set; } = new List<AttemptAnswer>();
}

public class AnswerOption : BaseEntity
{
    public Guid QuestionId { get; set; }
    public Question Question { get; set; } = null!;
    public string? OptionText { get; set; }
    public string? ImagePath { get; set; }
    public string? AudioPath { get; set; }
    public bool IsCorrect { get; set; }
    public int OrderIndex { get; set; }

    public ICollection<AttemptAnswer> AttemptAnswers { get; set; } = new List<AttemptAnswer>();
}