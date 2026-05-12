using System.Text;
using MCQ3.DataConnect.Data;
using MCQ3.DataConnect.Entities;
using MCQ3.DataConnect.Enums;
using MCQ3.DataConnect.Requests;
using Microsoft.EntityFrameworkCore;

namespace MCQ3.DataConnect.Services;

public class ExamService(AppDbContext dbContext)
{
    public async Task<IEnumerable<ViewModels.ExamViewModel>> GetByChapterAsync(Guid chapterId)
    {
        var exams = await dbContext.Exams
            .Where(e => e.ChapterId == chapterId)
            .Include(e => e.ExamQuestions)
                .ThenInclude(eq => eq.Question)
            .Include(e => e.Attempts)
            .OrderBy(e => e.CreatedAt)
            .ToListAsync();

        return exams.Select(MapToViewModel);
    }

    public async Task<IEnumerable<ViewModels.ExamViewModel>> GetPublishedAsync()
    {
        var exams = await dbContext.Exams
            .Where(e => e.Status == ExamStatus.Published)
            .Include(e => e.ExamQuestions)
            .Include(e => e.Attempts)
            .Include(e => e.Chapter)
                .ThenInclude(c => c!.Subject)
            .OrderBy(e => e.CreatedAt)
            .ToListAsync();

        return exams.Select(MapToViewModel);
    }

    public async Task<IEnumerable<ViewModels.StudentExamViewModel>> GetPublishedForStudentAsync(Guid studentId)
    {
        var now = DateTime.UtcNow;
        
        var exams = await dbContext.Exams
            .Where(e => e.Status == ExamStatus.Published)
            .Include(e => e.ExamQuestions)
            .Include(e => e.Attempts.Where(a => a.StudentId == studentId))
            .Include(e => e.Chapter)
                .ThenInclude(c => c!.Subject)
            .Include(e => e.Chapter!.Enrolments.Where(en => en.StudentId == studentId && en.RemovedAt == null && (en.ExpiresAt == null || en.ExpiresAt > now)))
            .OrderBy(e => e.CreatedAt)
            .ToListAsync();

        return exams.Select(e =>
        {
            var myAttempts = e.Attempts.ToList();
            var submittedAttempts = myAttempts.Where(a => a.SubmittedAt != null).ToList();
            var inProgress = myAttempts.FirstOrDefault(a => a.SubmittedAt == null);
            var bestScore = submittedAttempts.MaxBy(a => a.Score)?.Score;
            
            return new ViewModels.StudentExamViewModel(
                e.Id,
                e.Title,
                e.TotalMarks,
                e.PassingScore,
                e.TimeLimitSeconds,
                e.AvailableFrom,
                e.AvailableUntil,
                e.MaxAttempts,
                e.NegativeMarking,
                e.ShuffleQuestions,
                e.ShuffleOptions,
                e.ShowAnswersAfter,
                e.AutoReleaseResults,
                e.ChapterId,
                e.Chapter?.Title,
                e.Chapter?.Subject?.Title,
                e.CreatedAt,
                e.ExamQuestions?.Count ?? 0,
                myAttempts.Count,
                submittedAttempts.Count,
                inProgress != null,
                submittedAttempts.Any(a => a.IsPassed == true),
                submittedAttempts.Any(a => a.IsReleased) || (e.AutoReleaseResults && submittedAttempts.Any()),
                bestScore,
                myAttempts.MaxBy(a => a.StartedAt)?.StartedAt
            );
        });
    }

    public async Task<ViewModels.ExamViewModel?> GetByIdAsync(Guid id)
    {
        var exam = await dbContext.Exams
            .Include(e => e.ExamQuestions)
            .Include(e => e.Attempts)
            .FirstOrDefaultAsync(e => e.Id == id);

        return exam == null ? null : MapToViewModel(exam);
    }

    public async Task<ViewModels.ExamViewModel> CreateAsync(Guid chapterId, CreateExamRequest request)
    {
        var exam = new Exam
        {
            ChapterId = chapterId,
            Title = request.Title,
            TotalMarks = request.TotalMarks,
            PassingScore = request.PassingScore,
            TimeLimitSeconds = request.TimeLimitSeconds,
            AvailableFrom = request.AvailableFrom,
            AvailableUntil = request.AvailableUntil,
            MaxAttempts = request.MaxAttempts,
            NegativeMarking = request.NegativeMarking,
            ShuffleQuestions = request.ShuffleQuestions,
            ShuffleOptions = request.ShuffleOptions,
            ShowAnswersAfter = request.ShowAnswersAfter,
            AutoReleaseResults = request.AutoReleaseResults,
            Status = ExamStatus.Draft
        };

        dbContext.Exams.Add(exam);

        if (request.QuestionIds.Any())
        {
            int orderIndex = 0;
            foreach (var questionId in request.QuestionIds)
            {
                exam.ExamQuestions.Add(new ExamQuestion
                {
                    QuestionId = questionId,
                    OrderIndex = orderIndex++
                });
            }
        }

        await dbContext.SaveChangesAsync();
        return (await GetByIdAsync(exam.Id))!;
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateExamRequest request)
    {
        try
        {
            var exam = await dbContext.Exams
                .Include(e => e.ExamQuestions)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (exam == null) return false;

            if (request.Title != null) exam.Title = request.Title;
            if (request.TotalMarks.HasValue) exam.TotalMarks = request.TotalMarks.Value;
            if (request.PassingScore.HasValue) exam.PassingScore = request.PassingScore.Value;
            if (request.TimeLimitSeconds.HasValue) exam.TimeLimitSeconds = request.TimeLimitSeconds.Value;
            if (request.AvailableFrom.HasValue) exam.AvailableFrom = request.AvailableFrom;
            if (request.AvailableUntil.HasValue) exam.AvailableUntil = request.AvailableUntil;
            if (request.MaxAttempts.HasValue) exam.MaxAttempts = request.MaxAttempts.Value;
            if (request.NegativeMarking.HasValue) exam.NegativeMarking = request.NegativeMarking.Value;
            if (request.ShuffleQuestions.HasValue) exam.ShuffleQuestions = request.ShuffleQuestions.Value;
            if (request.ShuffleOptions.HasValue) exam.ShuffleOptions = request.ShuffleOptions.Value;
            if (request.ShowAnswersAfter.HasValue) exam.ShowAnswersAfter = request.ShowAnswersAfter.Value;
            if (request.AutoReleaseResults.HasValue) exam.AutoReleaseResults = request.AutoReleaseResults.Value;

            if (request.QuestionIds != null)
            {
                await dbContext.Database.ExecuteSqlRawAsync(
                    "DELETE FROM ExamQuestions WHERE ExamId = {0}", id);

                int orderIndex = 0;
                foreach (var questionId in request.QuestionIds)
                {
                    dbContext.ExamQuestions.Add(new ExamQuestion
                    {
                        ExamId = exam.Id,
                        QuestionId = questionId,
                        OrderIndex = orderIndex++
                    });
                }
            }

            await dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var exam = await dbContext.Exams.FindAsync(id);
        if (exam == null) return false;

        dbContext.Exams.Remove(exam);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> PublishAsync(Guid id)
    {
        var exam = await dbContext.Exams
            .Include(e => e.ExamQuestions)
            .FirstOrDefaultAsync(e => e.Id == id);
        
        if (exam == null || exam.Status != ExamStatus.Draft) return false;
        
        if (exam.ExamQuestions == null || exam.ExamQuestions.Count == 0)
            return false;
        
        if (exam.TotalMarks <= 0) return false;

        exam.Status = ExamStatus.Published;
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UnpublishAsync(Guid id)
    {
        var exam = await dbContext.Exams.FindAsync(id);
        if (exam == null || exam.Status != ExamStatus.Published) return false;

        exam.Status = ExamStatus.Draft;
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ArchiveAsync(Guid id)
    {
        var exam = await dbContext.Exams.FindAsync(id);
        if (exam == null) return false;

        exam.Status = ExamStatus.Archived;
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<ViewModels.ExamViewModel?> DuplicateAsync(Guid id, Guid? targetChapterId = null)
    {
        var original = await dbContext.Exams
            .Include(e => e.ExamQuestions)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (original == null) return null;

        var chapterId = targetChapterId ?? original.ChapterId;
        if (targetChapterId.HasValue)
        {
            var chapter = await dbContext.Chapters.FindAsync(targetChapterId);
            if (chapter == null) return null;
        }

        var duplicate = new Exam
        {
            ChapterId = chapterId,
            Title = $"{original.Title} (Copy)",
            TotalMarks = original.TotalMarks,
            PassingScore = original.PassingScore,
            TimeLimitSeconds = original.TimeLimitSeconds,
            AvailableFrom = null,
            AvailableUntil = null,
            MaxAttempts = original.MaxAttempts,
            NegativeMarking = original.NegativeMarking,
            ShuffleQuestions = original.ShuffleQuestions,
            ShuffleOptions = original.ShuffleOptions,
            ShowAnswersAfter = original.ShowAnswersAfter,
            AutoReleaseResults = original.AutoReleaseResults,
            Status = ExamStatus.Draft
        };

        dbContext.Exams.Add(duplicate);

        foreach (var eq in original.ExamQuestions.OrderBy(e => e.OrderIndex))
        {
            duplicate.ExamQuestions.Add(new ExamQuestion
            {
                QuestionId = eq.QuestionId,
                OrderIndex = eq.OrderIndex
            });
        }

        await dbContext.SaveChangesAsync();
        return await GetByIdAsync(duplicate.Id);
    }

    public async Task<IEnumerable<AttemptSummary>> GetSubmissionsAsync(Guid examId)
    {
        var attempts = await dbContext.Attempts
            .Include(a => a.Student)
            .Where(a => a.ExamId == examId && a.SubmittedAt != null)
            .OrderByDescending(a => a.StartedAt)
            .ToListAsync();

        return attempts.Select(a => new AttemptSummary(
            a.Id,
            a.StudentId!.Value,
            a.Student.FullName,
            a.AttemptNumber,
            a.StartedAt,
            a.SubmittedAt,
            a.Score,
            a.IsPassed
        ));
    }

    public async Task<bool> ReleaseResultsAsync(Guid examId, Guid? attemptId = null)
    {
        IQueryable<Attempt> query = dbContext.Attempts.Where(a => a.ExamId == examId && a.SubmittedAt != null);

        if (attemptId.HasValue)
        {
            query = query.Where(a => a.Id == attemptId.Value);
        }

        var attempts = await query.ToListAsync();
        foreach (var attempt in attempts)
        {
            attempt.IsReleased = true;
        }

        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<byte[]> ExportResultsAsync(Guid examId, string format)
    {
        var exam = await dbContext.Exams
            .Include(e => e.Attempts.Where(a => a.SubmittedAt != null))
                .ThenInclude(a => a.Student)
            .FirstOrDefaultAsync(e => e.Id == examId);

        if (exam == null) return Array.Empty<byte>();

        var sb = new StringBuilder();
        sb.AppendLine("Student Email,Student Name,Attempt Number,Started At,Submitted At,Score,Is Passed");

        foreach (var attempt in exam.Attempts)
        {
            sb.AppendLine($"{attempt.Student.Email},{attempt.Student.FullName},{attempt.AttemptNumber},{attempt.StartedAt},{attempt.SubmittedAt},{attempt.Score},{attempt.IsPassed}");
        }

        return Encoding.UTF8.GetBytes(sb.ToString());
    }

    private static ViewModels.ExamViewModel MapToViewModel(Exam e)
    {
        return new ViewModels.ExamViewModel(
            e.Id,
            e.Title,
            e.TotalMarks,
            e.PassingScore,
            e.TimeLimitSeconds,
            e.AvailableFrom,
            e.AvailableUntil,
            e.MaxAttempts,
            e.NegativeMarking,
            e.ShuffleQuestions,
            e.ShuffleOptions,
            e.ShowAnswersAfter,
            e.AutoReleaseResults,
            e.Status,
            e.ChapterId,
            e.Chapter?.Title,
            e.Chapter?.Subject?.Title,
            e.CreatedAt,
            e.ExamQuestions?.Count ?? 0,
            e.Attempts?.Count(a => a.SubmittedAt != null) ?? 0,
            e.ExamQuestions?.Select(eq => new ViewModels.ExamQuestionViewModel(
                eq.QuestionId,
                eq.Question?.StemText,
                eq.Question?.Difficulty?.ToString()
            )).ToList()
        );
    }
}

public record AttemptSummary(
    Guid AttemptId,
    Guid StudentId,
    string StudentName,
    int AttemptNumber,
    DateTime StartedAt,
    DateTime? SubmittedAt,
    decimal? Score,
    bool? IsPassed
);