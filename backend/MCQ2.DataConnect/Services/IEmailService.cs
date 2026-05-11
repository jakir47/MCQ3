namespace MCQ3.DataConnect.Services;

public interface IEmailService
{
    Task<bool> SendEmailAsync(string to, string subject, string body, bool isHtml = false);
    Task<bool> SendEmailAsync(string to, string subject, string body, Dictionary<string, byte[]> attachments);
    Task<bool> SendWelcomeEmailAsync(string email, string fullName, string temporaryPassword);
    Task<bool> SendVerificationEmailAsync(string email, string fullName, string token);
    Task<bool> SendPasswordResetEmailAsync(string email, string fullName, string token);
    Task<bool> SendEnrolmentNotificationAsync(string email, string fullName, string chapterName, string subjectName);
    Task<bool> SendExamAvailableNotificationAsync(string email, string fullName, string examTitle, string chapterName);
    Task<bool> SendResultReleasedNotificationAsync(string email, string fullName, string examTitle, decimal score, bool passed);
}