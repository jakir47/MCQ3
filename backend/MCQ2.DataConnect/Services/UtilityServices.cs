using System.Text.Json;
using MCQ3.DataConnect.Data;
using MCQ3.DataConnect.Entities;
using MCQ3.DataConnect.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MCQ3.DataConnect.Services;

public class UploadService(IConfiguration configuration, ILogger<UploadService> logger)
{
    private readonly ILogger<UploadService> _logger = logger;

    public async Task<string> UploadImageAsync(IFormFile file, Guid teacherId)
    {
        var maxSize = int.Parse(configuration["FileStorage:MaxImageSizeMB"] ?? "5") * 1024 * 1024;
        if (file.Length > maxSize)
            throw new InvalidOperationException("File too large");

        return await SaveFileAsync(file, "images", teacherId);
    }

    public async Task<string> UploadAudioAsync(IFormFile file, Guid teacherId)
    {
        var maxSize = int.Parse(configuration["FileStorage:MaxAudioSizeMB"] ?? "25") * 1024 * 1024;
        if (file.Length > maxSize)
            throw new InvalidOperationException("File too large");

        return await SaveFileAsync(file, "audio", teacherId);
    }

    private async Task<string> SaveFileAsync(IFormFile file, string category, Guid teacherId)
    {
        var rootPath = configuration["FileStorage:UploadPath"] ?? @"C:\MCQ2\Uploads";
        var dir = Path.Combine(rootPath, teacherId.ToString(), category);
        Directory.CreateDirectory(dir);

        var ext = Path.GetExtension(file.FileName).ToLower();
        var name = $"{Guid.NewGuid()}{ext}";
        var path = Path.Combine(dir, name);

        await using var fs = new FileStream(path, FileMode.Create);
        await file.CopyToAsync(fs);

        return $"/uploads/{teacherId}/{category}/{name}";
    }

    public Task<bool> DeleteFileAsync(string relativePath)
    {
        var rootPath = configuration["FileStorage:UploadPath"] ?? @"C:\MCQ2\Uploads";
        var fullPath = Path.Combine(rootPath, relativePath.Replace("/uploads/", "").Replace('/', '\\'));
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }
}

public class AnalyticsService(AppDbContext dbContext)
{
    public async Task<ExamSummaryResponse> GetExamSummaryAsync(Guid examId)
    {
        var attempts = await dbContext.Attempts
            .Where(a => a.ExamId == examId && a.SubmittedAt != null)
            .ToListAsync();

        return new ExamSummaryResponse(
            examId, "", attempts.Count, attempts.Count,
            attempts.Count(a => a.IsPassed == true), attempts.Count(a => a.IsPassed == false),
            attempts.Average(a => a.Score ?? 0), attempts.Max(a => a.Score ?? 0), attempts.Min(a => a.Score ?? 0)
        );
    }

    public async Task<IEnumerable<QuestionAnalyticsResponse>> GetQuestionAnalyticsAsync(Guid examId)
    {
        var examQuestions = await dbContext.ExamQuestions
            .Include(eq => eq.Question)
                .ThenInclude(q => q.AttemptAnswers)
            .Where(eq => eq.ExamId == examId)
            .ToListAsync();

        return examQuestions.Select(eq => new QuestionAnalyticsResponse(
            eq.QuestionId, eq.Question.StemText ?? "",
            eq.Question.AttemptAnswers.Count,
            eq.Question.AttemptAnswers.Count(aa => aa.IsCorrect == true),
            eq.Question.AttemptAnswers.Count(aa => aa.IsCorrect == false),
            eq.Question.AttemptAnswers.Count(aa => aa.SelectedOptionId == null),
            eq.Question.AttemptAnswers.Count > 0
                ? (decimal)eq.Question.AttemptAnswers.Count(aa => aa.IsCorrect == true) / eq.Question.AttemptAnswers.Count * 100
                : 0
        ));
    }

    public async Task<ScoreDistributionResponse> GetScoreDistributionAsync(Guid examId)
    {
        var attempts = await dbContext.Attempts
            .Where(a => a.ExamId == examId && a.SubmittedAt != null && a.Score.HasValue)
            .ToListAsync();

        var ranges = new List<int> { 0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };
        var counts = new int[10];
        var exam = await dbContext.Exams.FindAsync(examId);

        foreach (var a in attempts)
        {
            var percentage = exam?.TotalMarks > 0 ? (int)((a.Score!.Value / exam.TotalMarks) * 100) : 0;
            var index = Math.Min(percentage / 10, 9);
            counts[index]++;
        }

        return new ScoreDistributionResponse(ranges, counts.ToList());
    }

    public async Task<IEnumerable<StudentProgressResponse>> GetChapterStudentsAsync(Guid chapterId)
    {
        return await dbContext.Enrolments
            .Include(e => e.Student)
                .ThenInclude(s => s.Attempts)
            .Where(e => e.ChapterId == chapterId && e.RemovedAt == null)
            .Select(e => new StudentProgressResponse(
                e.StudentId!.Value, e.Student.FullName, e.Student.Email,
                e.Student.Attempts.Count,
                e.Student.Attempts.Max(a => a.Score) ?? 0,
                e.Student.Attempts.Average(a => a.Score) ?? 0,
                e.Student.Attempts.Max(a => a.StartedAt)
            )).ToListAsync();
    }

    public async Task<IEnumerable<TrendResponse>> GetChapterTrendsAsync(Guid chapterId)
    {
        var attempts = await dbContext.Attempts
            .Include(a => a.Exam)
            .Where(a => a.Exam.ChapterId == chapterId && a.SubmittedAt != null)
            .OrderBy(a => a.SubmittedAt)
            .ToListAsync();

        return attempts.GroupBy(a => a.SubmittedAt!.Value.Date)
            .Select(g => new TrendResponse(g.Key, g.Count(), g.Average(a => a.Score ?? 0)));
    }

    public async Task<EnrolmentAnalyticsResponse> GetEnrolmentAnalyticsAsync(Guid chapterId)
    {
        var enrolments = await dbContext.Enrolments
            .Where(e => e.ChapterId == chapterId)
            .ToListAsync();

        var now = DateTime.UtcNow;
        var monthlyEnrolments = enrolments
            .GroupBy(e => e.EnrolledAt.Month)
            .Select(g => new MonthlyEnrolment(g.Key, g.Count()))
            .ToList();

        return new EnrolmentAnalyticsResponse(
            enrolments.Count(e => e.RemovedAt == null),
            enrolments.Count(e => e.RemovedAt == null && (e.ExpiresAt == null || e.ExpiresAt > now)),
            enrolments.Count(e => e.RemovedAt == null && e.ExpiresAt.HasValue && e.ExpiresAt <= now),
            enrolments.Count(e => e.RemovedAt != null),
            monthlyEnrolments
        );
    }
}

public class NotificationService(AppDbContext dbContext)
{
    public async Task<IEnumerable<NotificationViewModel>> GetByUserAsync(Guid userId)
    {
        return await dbContext.Notifications
            .Where(n => n.UserId == userId)
            .OrderByDescending(n => n.CreatedAt)
            .Select(n => new NotificationViewModel(n.Id, n.Type, n.Title, n.Body, n.IsRead, n.CreatedAt))
            .ToListAsync();
    }

    public async Task<bool> MarkAsReadAsync(Guid notificationId)
    {
        var notification = await dbContext.Notifications.FindAsync(notificationId);
        if (notification == null) return false;
        notification.IsRead = true;
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> MarkAllAsReadAsync(Guid userId)
    {
        var notifications = await dbContext.Notifications
            .Where(n => n.UserId == userId && !n.IsRead)
            .ToListAsync();
        foreach (var n in notifications) n.IsRead = true;
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task CreateAsync(Guid userId, string type, string title, string body, Guid? entityId = null)
    {
        var notification = new Notification
        {
            UserId = userId,
            Type = type,
            Title = title,
            Body = body,
            EntityId = entityId
        };
        dbContext.Notifications.Add(notification);
        await dbContext.SaveChangesAsync();
    }

    public async Task<int> GetUnreadCountAsync(Guid userId)
    {
        return await dbContext.Notifications.CountAsync(n => n.UserId == userId && !n.IsRead);
    }

    public async Task<bool> DeleteAsync(Guid notificationId)
    {
        var notification = await dbContext.Notifications.FindAsync(notificationId);
        if (notification == null) return false;
        dbContext.Notifications.Remove(notification);
        await dbContext.SaveChangesAsync();
        return true;
    }
}

public class AuditLogService(AppDbContext dbContext)
{
    public async Task LogAsync(Guid actorId, string action, string targetType, Guid targetId, object? metadata = null)
    {
        var log = new AuditLog
        {
            ActorId = actorId,
            Action = action,
            TargetType = targetType,
            TargetId = targetId,
            Metadata = metadata != null ? JsonSerializer.Serialize(metadata) : null
        };
        dbContext.AuditLogs.Add(log);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<AuditLogViewModel>> GetByTargetAsync(string targetType, Guid targetId)
    {
        return await dbContext.AuditLogs
            .Where(l => l.TargetType == targetType && l.TargetId == targetId)
            .OrderByDescending(l => l.CreatedAt)
            .Select(l => new AuditLogViewModel(l.Id, l.ActorId, l.Actor.FullName, l.Action, l.TargetType, l.TargetId, l.Metadata, l.CreatedAt))
            .ToListAsync();
    }

    public async Task<IEnumerable<AuditLogViewModel>> GetByActorAsync(Guid actorId, int page = 1, int pageSize = 20)
    {
        return await dbContext.AuditLogs
            .Where(l => l.ActorId == actorId)
            .OrderByDescending(l => l.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(l => new AuditLogViewModel(l.Id, l.ActorId, l.Actor.FullName, l.Action, l.TargetType, l.TargetId, l.Metadata, l.CreatedAt))
            .ToListAsync();
    }

    public async Task<byte[]> ExportAsync(DateTime? from = null, DateTime? to = null)
    {
        var query = dbContext.AuditLogs.AsQueryable();
        if (from.HasValue) query = query.Where(l => l.CreatedAt >= from.Value);
        if (to.HasValue) query = query.Where(l => l.CreatedAt <= to.Value);

        var logs = await query.OrderByDescending(l => l.CreatedAt).ToListAsync();

        var sb = new System.Text.StringBuilder();
        sb.AppendLine("Timestamp,Actor,Action,Target Type,Target ID,Metadata");
        foreach (var l in logs)
        {
            sb.AppendLine($"{l.CreatedAt:yyyy-MM-dd HH:mm:ss},{l.Actor?.FullName ?? "System"},{l.Action},{l.TargetType},{l.TargetId},{l.Metadata?.Replace(",", ";") ?? ""}");
        }
        return System.Text.Encoding.UTF8.GetBytes(sb.ToString());
    }
}

public record ExamSummaryResponse(Guid ExamId, string ExamTitle, int TotalStudents, int TotalAttempts, int PassedCount, int FailedCount, decimal AverageScore, decimal HighestScore, decimal LowestScore);
public record QuestionAnalyticsResponse(Guid QuestionId, string QuestionStem, int TotalAttempts, int CorrectCount, int IncorrectCount, int SkippedCount, decimal SuccessRate);
public record ScoreDistributionResponse(List<int> Ranges, List<int> Counts);
public record StudentProgressResponse(Guid StudentId, string StudentName, string StudentEmail, int TotalAttempts, decimal BestScore, decimal AverageScore, DateTime? LastAttemptAt);
public record TrendResponse(DateTime Date, int Attempts, decimal AverageScore);
public record EnrolmentAnalyticsResponse(int TotalStudents, int ActiveStudents, int ExpiredStudents, int RemovedStudents, List<MonthlyEnrolment> MonthlyEnrolments);