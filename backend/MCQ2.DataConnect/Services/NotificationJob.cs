using MCQ3.DataConnect.Data;
using MCQ3.DataConnect.Entities;
using MCQ3.DataConnect.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MCQ3.DataConnect.Services;

public class NotificationJob(IServiceProvider serviceProvider, ILogger<NotificationJob> logger)
    : BackgroundService
{
    private readonly TimeSpan _interval = TimeSpan.FromHours(1);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await ProcessNotificationsAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing notifications");
            }

            await Task.Delay(_interval, stoppingToken);
        }
    }

    private async Task ProcessNotificationsAsync()
    {
        try
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var settingsService = scope.ServiceProvider.GetRequiredService<SettingsService>();
            var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

            var expiryReminderDays = int.Parse(await settingsService.GetAsync("ExpiryReminderDays") ?? "7");
            var examClosingHours = int.Parse(await settingsService.GetAsync("ExamClosingReminderHours") ?? "24");
            var enableEnrolment = (await settingsService.GetAsync("EnableEnrolmentNotifications")).ParseBool();
            var enableExam = (await settingsService.GetAsync("EnableExamNotifications")).ParseBool();
            var enableResult = (await settingsService.GetAsync("EnableResultNotifications")).ParseBool();

            if (enableEnrolment)
            {
                await ProcessExpiryRemindersAsync(dbContext, emailService, expiryReminderDays);
            }

            if (enableExam)
            {
                await ProcessExamClosingRemindersAsync(dbContext, emailService, examClosingHours);
            }
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Failed to process notifications - will retry on next cycle");
        }
    }

    private async Task ProcessExpiryRemindersAsync(AppDbContext dbContext, IEmailService emailService, int daysBefore)
    {
        var cutoff = DateTime.UtcNow.AddDays(daysBefore);
        var expiringEnrolments = await dbContext.Enrolments
            .Include(e => e.Student)
            .Include(e => e.Chapter)
            .Where(e => e.RemovedAt == null && e.ExpiresAt != null && e.ExpiresAt <= cutoff && e.ExpiresAt > DateTime.UtcNow)
            .Where(e => !dbContext.Notifications.Any(n => n.UserId == e.StudentId && n.Type == "ExpiryReminder" && n.EntityId == e.Id))
            .ToListAsync();

        foreach (var enrolment in expiringEnrolments)
        {
            try
            {
                await emailService.SendEmailAsync(
                    enrolment.Student.Email,
                    $"Enrolment expiring soon: {enrolment.Chapter.Title}",
                    $"Dear {enrolment.Student.FullName},\n\nYour enrolment in chapter '{enrolment.Chapter.Title}' will expire on {enrolment.ExpiresAt:yyyy-MM-dd}. Please contact your teacher if you need an extension."
                );

                dbContext.Notifications.Add(new Notification
                {
                    UserId = enrolment.StudentId,
                    Type = "ExpiryReminder",
                    Title = "Enrolment Expiry Reminder",
                    Body = $"Your enrolment in '{enrolment.Chapter.Title}' expires on {enrolment.ExpiresAt:yyyy-MM-dd}",
                    EntityId = enrolment.Id,
                    CreatedAt = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Failed to send expiry reminder for enrolment {EnrolmentId}", enrolment.Id);
            }
        }

        await dbContext.SaveChangesAsync();
    }

    private async Task ProcessExamClosingRemindersAsync(AppDbContext dbContext, IEmailService emailService, int hoursBefore)
    {
        var cutoff = DateTime.UtcNow.AddHours(hoursBefore);
        var upcomingExams = await dbContext.Exams
            .Include(e => e.Chapter)
                .ThenInclude(c => c!.Enrolments)
                    .ThenInclude(en => en.Student)
            .Where(e => e.Status == ExamStatus.Published && e.AvailableUntil != null && e.AvailableUntil <= cutoff && e.AvailableUntil > DateTime.UtcNow)
            .ToListAsync();

        foreach (var exam in upcomingExams)
        {
            var enrolledStudents = exam.Chapter!.Enrolments.Where(e => e.RemovedAt == null && (e.ExpiresAt == null || e.ExpiresAt > DateTime.UtcNow)).Select(e => e.Student);

            foreach (var student in enrolledStudents)
            {
                var alreadyNotified = await dbContext.Notifications.AnyAsync(n => n.UserId == student.Id && n.Type == "ExamClosing" && n.EntityId == exam.Id);
                if (alreadyNotified) continue;

                try
                {
                    await emailService.SendExamAvailableNotificationAsync(student.Email, student.FullName, exam.Title, exam.Chapter.Title);

                    dbContext.Notifications.Add(new Notification
                    {
                        UserId = student.Id,
                        Type = "ExamClosing",
                        Title = "Exam Closing Soon",
                        Body = $"Exam '{exam.Title}' will close on {exam.AvailableUntil:yyyy-MM-dd HH:mm}",
                        EntityId = exam.Id,
                        CreatedAt = DateTime.UtcNow
                    });
                }
                catch (Exception ex)
                {
                    logger.LogWarning(ex, "Failed to send exam closing reminder for exam {ExamId} to student {StudentId}", exam.Id, student.Id);
                }
            }
        }

        await dbContext.SaveChangesAsync();
    }
}

public static class StringExtensions
{
    public static bool ParseBool(this string? value)
    {
        return bool.TryParse(value, out var result) && result;
    }
}