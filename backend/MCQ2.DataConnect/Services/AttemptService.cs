using System.Text.Json;
using MCQ3.DataConnect.Data;
using MCQ3.DataConnect.Entities;
using MCQ3.DataConnect.Enums;
using MCQ3.DataConnect.Requests;
using MCQ3.DataConnect.Responses;
using MCQ3.DataConnect.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace MCQ3.DataConnect.Services;

public class AttemptService(AppDbContext dbContext)
{
    public async Task<IEnumerable<ChapterWithExamsViewModel>> GetMyChaptersAsync(Guid studentId)
    {
        var now = DateTime.UtcNow;
        var enrolments = await dbContext.Enrolments
            .Include(e => e.Chapter)
                .ThenInclude(c => c.Subject)
            .Include(e => e.Exam)
            .Where(e => e.StudentId == studentId && e.RemovedAt == null &&
                (e.ExpiresAt == null || e.ExpiresAt > now) && e.ExamId != null)
            .ToListAsync();

        return enrolments.Select(e => new ChapterWithExamsViewModel(
            e.Chapter.Id,
            e.Chapter.Title,
            e.Chapter.SubjectId,
            e.Chapter.Subject.Title,
            e.Exam != null && e.Exam.Status == ExamStatus.Published &&
                (!e.Exam.AvailableFrom.HasValue || e.Exam.AvailableFrom <= now) &&
                (!e.Exam.AvailableUntil.HasValue || e.Exam.AvailableUntil >= now)
                ? new List<ExamSummary> { new ExamSummary(
                    e.Exam.Id, e.Exam.Title, e.Exam.TimeLimitSeconds ?? 0, true, e.Exam.MaxAttempts, e.Exam.Attempts.Count
                ) }
                : new List<ExamSummary>()
        ));
    }

    public async Task<IEnumerable<object>> GetMyAttemptsAsync(Guid studentId)
    {
        var attempts = await dbContext.Attempts
            .Include(a => a.Exam)
                .ThenInclude(e => e!.Chapter)
                    .ThenInclude(c => c!.Subject)
            .Where(a => a.StudentId == studentId && a.SubmittedAt != null)
            .OrderByDescending(a => a.SubmittedAt)
            .ToListAsync();

        return attempts.Select(a => new
        {
            id = a.Id,
            examTitle = a.Exam?.Title,
            subjectName = a.Exam?.Chapter?.Subject?.Title,
            startedAt = a.StartedAt,
            submittedAt = a.SubmittedAt,
            score = a.Score,
            isPassed = a.IsPassed,
            totalQuestions = a.Exam?.ExamQuestions?.Count ?? 0,
            correctAnswers = a.Score > 0 && a.Exam?.PassingScore > 0 
                ? (int)((a.Score / 100) * (a.Exam.ExamQuestions?.Count ?? 0)) 
                : 0,
            timeTakenMinutes = a.TimeSpentSecs.HasValue ? (int)(a.TimeSpentSecs.Value / 60) : 0
        });
    }

    public async Task<StartExamResult> StartExamAsync(Guid examId, Guid studentId)
    {
        var exam = await dbContext.Exams
            .Include(e => e.ExamQuestions)
                .ThenInclude(eq => eq.Question)
                    .ThenInclude(q => q.AnswerOptions)
            .FirstOrDefaultAsync(e => e.Id == examId);

        if (exam == null)
            return StartExamResult.Fail("EXAM_NOT_FOUND", "Exam not found");

        if (exam.Status != ExamStatus.Published)
            return StartExamResult.Fail("EXAM_NOT_AVAILABLE", "Exam is not available for taking");

        var now = DateTime.UtcNow;
        if (exam.AvailableFrom.HasValue && exam.AvailableFrom > now)
            return StartExamResult.Fail("EXAM_NOT_STARTED", $"Exam will be available from {exam.AvailableFrom.Value.ToLocalTime():g}");

        if (exam.AvailableUntil.HasValue && exam.AvailableUntil < now)
            return StartExamResult.Fail("EXAM_EXPIRED", "Exam has expired and is no longer available");

        var enrolment = await dbContext.Enrolments
            .FirstOrDefaultAsync(en => en.StudentId == studentId && en.ExamId == examId &&
                en.RemovedAt == null && (en.ExpiresAt == null || en.ExpiresAt > now));
        if (enrolment == null)
            return StartExamResult.Fail("NOT_ENROLLED", "You are not enrolled in this exam. Please contact your teacher.");

        if (exam.MaxAttempts.HasValue)
        {
            var attemptCount = await dbContext.Attempts
                .CountAsync(a => a.ExamId == examId && a.StudentId == studentId && a.SubmittedAt != null);
            if (attemptCount >= exam.MaxAttempts.Value)
                return StartExamResult.Fail("MAX_ATTEMPTS_REACHED", $"You have already used all {exam.MaxAttempts.Value} attempts for this exam");
        }

        var inProgress = await dbContext.Attempts
            .FirstOrDefaultAsync(a => a.ExamId == examId && a.StudentId == studentId && a.SubmittedAt == null);

        if (inProgress != null)
        {
            var existingAttempt = await GetAttemptAsync(inProgress.Id);
            return existingAttempt != null 
                ? StartExamResult.Ok(existingAttempt)
                : StartExamResult.Fail("RESUME_FAILED", "Failed to resume your previous attempt");
        }

        var lastAttempt = await dbContext.Attempts
            .Where(a => a.ExamId == examId && a.StudentId == studentId)
            .OrderByDescending(a => a.AttemptNumber)
            .FirstOrDefaultAsync();

        var newAttempt = new Attempt
        {
            ExamId = examId,
            StudentId = studentId,
            AttemptNumber = (lastAttempt?.AttemptNumber ?? 0) + 1,
            StartedAt = DateTime.UtcNow
        };

        var questionOrder = exam.ShuffleQuestions
            ? exam.ExamQuestions.Select(eq => eq.QuestionId).OrderBy(_ => Guid.NewGuid()).ToList()
            : exam.ExamQuestions.Select(eq => eq.QuestionId).ToList();

        var optionOrders = new Dictionary<Guid, List<Guid>>();
        if (exam.ShuffleOptions)
        {
            foreach (var eq in exam.ExamQuestions)
            {
                optionOrders[eq.QuestionId] = eq.Question.AnswerOptions
                    .OrderBy(_ => Guid.NewGuid())
                    .Select(o => o.Id).ToList();
            }
        }

        newAttempt.ResumeData = JsonSerializer.Serialize(new
        {
            answers = new Dictionary<Guid, Guid>(),
            remainingSecs = exam.TimeLimitSeconds ?? 0,
            questionOrder = questionOrder,
            optionOrders = optionOrders
        });

        dbContext.Attempts.Add(newAttempt);
        await dbContext.SaveChangesAsync();

        var savedAttempt = await GetAttemptAsync(newAttempt.Id);
        return savedAttempt != null 
            ? StartExamResult.Ok(savedAttempt)
            : StartExamResult.Fail("START_FAILED", "Failed to start exam");
    }

    public async Task<AttemptViewModel?> GetAttemptAsync(Guid attemptId)
    {
        var attempt = await dbContext.Attempts
            .Include(a => a.Student)
            .Include(a => a.Exam)
                .ThenInclude(e => e.ExamQuestions)
                    .ThenInclude(eq => eq.Question)
                        .ThenInclude(q => q.AnswerOptions)
            .Include(a => a.AttemptAnswers)
                .ThenInclude(aa => aa.Question)
            .FirstOrDefaultAsync(a => a.Id == attemptId);

        if (attempt == null) return null;

        var examQuestions = attempt.Exam?.ExamQuestions?.Select(eq => new ExamQuestionInfo(
            eq.QuestionId,
            eq.Question?.StemText ?? "",
            eq.Question?.AnswerOptions?.Select(o => new AnswerOptionInfo(o.Id, o.OptionText ?? "")).ToList() ?? new List<AnswerOptionInfo>()
        )).ToList() ?? new List<ExamQuestionInfo>();

        var examInfo = attempt.Exam != null ? new ExamInfo(
            attempt.Exam.Id,
            attempt.Exam.Title,
            attempt.Exam.TimeLimitSeconds,
            attempt.Exam.ShuffleQuestions,
            attempt.Exam.ShuffleOptions,
            examQuestions
        ) : null;

        return new AttemptViewModel(
            attempt.Id, attempt.ExamId, attempt.Exam?.Title ?? "", attempt.StudentId!.Value,
            attempt.Student?.FullName ?? "", attempt.AttemptNumber, attempt.StartedAt,
            attempt.SubmittedAt, attempt.TimeSpentSecs, attempt.Score, attempt.IsPassed,
            attempt.IsReleased, attempt.AutoSubmitted,
            attempt.AttemptAnswers.Select(aa => new AttemptAnswerViewModel(
                aa.Id, aa.QuestionId, aa.Question?.StemText ?? "",
                aa.SelectedOptionId, aa.SelectedOption?.OptionText, aa.IsCorrect, aa.MarksAwarded
            )).ToList(),
            examInfo
        );
    }

    public async Task<bool> SaveAsync(Guid attemptId, SaveAttemptRequest request)
    {
        var attempt = await dbContext.Attempts.FindAsync(attemptId);
        if (attempt == null || attempt.SubmittedAt != null) return false;

        var resumeData = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(attempt.ResumeData ?? "{}");
        if (resumeData == null) return false;

        resumeData["answers"] = JsonSerializer.Deserialize<JsonElement>(JsonSerializer.Serialize(request.Answers));
        resumeData["remainingSecs"] = JsonSerializer.Deserialize<JsonElement>(JsonSerializer.Serialize(request.RemainingSecs));
        if (request.QuestionOrder != null)
            resumeData["questionOrder"] = JsonSerializer.Deserialize<JsonElement>(JsonSerializer.Serialize(request.QuestionOrder));
        if (request.OptionOrders != null)
            resumeData["optionOrders"] = JsonSerializer.Deserialize<JsonElement>(JsonSerializer.Serialize(request.OptionOrders));

        attempt.ResumeData = JsonSerializer.Serialize(resumeData);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<AttemptResultResponse?> SubmitAsync(Guid attemptId, SubmitAttemptRequest request)
    {
        var attempt = await dbContext.Attempts
            .Include(a => a.Exam)
                .ThenInclude(e => e.ExamQuestions)
                    .ThenInclude(eq => eq.Question)
                        .ThenInclude(q => q.AnswerOptions)
            .FirstOrDefaultAsync(a => a.Id == attemptId);


        if (attempt == null || attempt.SubmittedAt != null) return null;

        attempt.SubmittedAt = DateTime.UtcNow;
        attempt.TimeSpentSecs = request.TimeSpentSecs;

        var correctAnswers = attempt.Exam.ExamQuestions
            .SelectMany(eq => eq.Question.AnswerOptions.Where(o => o.IsCorrect))
            .ToDictionary(o => o.QuestionId, o => o.Id);

        Console.WriteLine($"DEBUG: ExamQuestions count: {attempt.Exam.ExamQuestions.Count}");
        Console.WriteLine($"DEBUG: Request answers count: {request.Answers.Count}");
        Console.WriteLine($"DEBUG: Request answers: {System.Text.Json.JsonSerializer.Serialize(request.Answers)}");

        decimal score = 0;
        int correctCount = 0, incorrectCount = 0, skippedCount = 0;

      

        foreach (var eq in attempt.Exam.ExamQuestions)
        {
            var questionIdStr = eq.QuestionId.ToString();
            bool hasSelected = request.Answers.TryGetValue(questionIdStr, out var selectedOptionId);
            var isCorrect = hasSelected && correctAnswers.GetValueOrDefault(eq.QuestionId) == selectedOptionId;
            var marksAwarded = isCorrect ? eq.Question.PositiveMarks :
                (hasSelected && attempt.Exam.NegativeMarking ? -eq.Question.NegativeMarks : 0);

            if (!hasSelected) skippedCount++;
            else if (isCorrect) correctCount++;
            else incorrectCount++;

            if (isCorrect) score += eq.Question.PositiveMarks;
            else if (hasSelected && attempt.Exam.NegativeMarking) score -= eq.Question.NegativeMarks;

            attempt.AttemptAnswers.Add(new AttemptAnswer
            {
                QuestionId = eq.QuestionId,
                SelectedOptionId = hasSelected ? selectedOptionId : null,
                IsCorrect = isCorrect,
                MarksAwarded = marksAwarded
            });
        }

        score = Math.Max(0, score);
        attempt.Score = score;
        attempt.IsPassed = score >= attempt.Exam.PassingScore;

        if (attempt.Exam.AutoReleaseResults) attempt.IsReleased = true;

        attempt.ResumeData = JsonSerializer.Serialize(new
        {
            answers = request.Answers,
            remainingSecs = 0,
            questionOrder = attempt.Exam.ExamQuestions.Select(eq => eq.QuestionId).ToList(),
            optionOrders = new Dictionary<Guid, List<Guid>>()
        });

        try
        {
            await dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return new AttemptResultResponse(
            attempt.Id, score, attempt.IsPassed.Value, attempt.IsReleased,
            attempt.Exam.ExamQuestions.Count, correctCount, incorrectCount, skippedCount, new List<QuestionResultViewModel>()
        );
    }

    public async Task<AttemptResultResponse?> GetResultAsync(Guid attemptId)
    {
        var attempt = await dbContext.Attempts
            .Include(a => a.AttemptAnswers)
            .Include(a => a.Exam)
            .Include(a => a.AttemptAnswers)
            .ThenInclude(aa => aa.Question).ThenInclude(question => question.AnswerOptions)
            .Include(a => a.AttemptAnswers)
                .ThenInclude(aa => aa.SelectedOption)
            .FirstOrDefaultAsync(a => a.Id == attemptId);

        if (attempt == null || !attempt.IsReleased) return null;

        var questionResults = attempt.AttemptAnswers.Select(aa => new QuestionResultViewModel(
            aa.QuestionId,
            aa.Question.StemText ?? "",
            aa.SelectedOptionId,
            aa.SelectedOption?.OptionText,
            aa.Question.AnswerOptions.FirstOrDefault(o => o.IsCorrect)?.OptionText,
            aa.IsCorrect,
            aa.MarksAwarded ?? 0,
            aa.Question.Explanation
        )).ToList();

        return new AttemptResultResponse(
            attempt.Id, attempt.Score ?? 0, attempt.IsPassed ?? false, attempt.IsReleased,
            attempt.AttemptAnswers.Count,
            attempt.AttemptAnswers.Count(aa => aa.IsCorrect == true),
            attempt.AttemptAnswers.Count(aa => aa.IsCorrect == false),
            attempt.AttemptAnswers.Count(aa => aa.SelectedOptionId == null),
            questionResults
        );
    }

    public async Task<ExamReviewViewModel?> GetReviewAsync(Guid attemptId, Guid studentId)
    {
        var attempt = await dbContext.Attempts
            .Include(a => a.Exam)
                .ThenInclude(e => e.Chapter)
                    .ThenInclude(c => c!.Subject)
            .Include(a => a.Exam)
                .ThenInclude(e => e.ExamQuestions)
                    .ThenInclude(eq => eq.Question)
                        .ThenInclude(q => q.AnswerOptions)
            .Include(a => a.AttemptAnswers)
                .ThenInclude(aa => aa.Question)
                    .ThenInclude(q => q.AnswerOptions)
            .Include(a => a.AttemptAnswers)
                .ThenInclude(aa => aa.SelectedOption)
            .FirstOrDefaultAsync(a => a.Id == attemptId && a.StudentId == studentId);

        if (attempt == null || !attempt.IsReleased) return null;

        Console.WriteLine($"DEBUG GetReview: AttemptAnswers count: {attempt.AttemptAnswers.Count}");
        Console.WriteLine($"DEBUG GetReview: Exam.ExamQuestions count: {attempt.Exam?.ExamQuestions?.Count ?? 0}");

        var questionReviews = new List<QuestionReviewItem>();
        
        foreach (var aa in attempt.AttemptAnswers)
        {
            if (aa.Question == null) continue;
            
            var correctOption = aa.Question.AnswerOptions?.FirstOrDefault(o => o.IsCorrect);
            var options = (aa.Question.AnswerOptions ?? Enumerable.Empty<AnswerOption>()).Select(o => new OptionReviewItem(
                o.Id,
                o.OptionText ?? "",
                o.IsCorrect,
                aa.SelectedOptionId == o.Id
            )).ToList();

            questionReviews.Add(new QuestionReviewItem(
                aa.QuestionId,
                aa.Question.StemText ?? "",
                options,
                aa.SelectedOptionId,
                aa.SelectedOption?.OptionText,
                correctOption?.Id,
                correctOption?.OptionText,
                aa.IsCorrect ?? false,
                aa.MarksAwarded ?? 0,
                aa.Question.Explanation
            ));
        }

        return new ExamReviewViewModel(
            attempt.Id,
            attempt.Exam?.Title ?? "",
            attempt.Exam?.Chapter?.Subject?.Title ?? "",
            attempt.Score ?? 0,
            attempt.IsPassed ?? false,
            attempt.AttemptAnswers.Count,
            attempt.AttemptAnswers.Count(aa => aa.IsCorrect == true),
            attempt.AttemptAnswers.Count(aa => aa.IsCorrect == false),
            attempt.AttemptAnswers.Count(aa => aa.SelectedOptionId == null),
            questionReviews
        );
    }

    public async Task<bool> ReleaseResultAsync(Guid attemptId)
    {
        var attempt = await dbContext.Attempts.FindAsync(attemptId);
        if (attempt == null) return false;
        attempt.IsReleased = true;
        await dbContext.SaveChangesAsync();
        return true;
    }
}

public record AttemptResultResponse(
    Guid AttemptId, decimal Score, bool IsPassed, bool IsReleased,
    int TotalQuestions, int CorrectCount, int IncorrectCount, int SkippedCount,
    List<QuestionResultViewModel> QuestionResults
);

public record QuestionResultViewModel(
    Guid QuestionId, string Stem, Guid? SelectedOptionId, string? SelectedOptionText,
    string? CorrectOptionText, bool? IsCorrect, decimal MarksAwarded, string? Explanation
);