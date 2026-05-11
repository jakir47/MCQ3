using System.Net;
using System.Net.Mail;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MCQ3.DataConnect.Services;

public class EmailService(IConfiguration configuration, ILogger<EmailService> logger) : IEmailService
{
    public async Task<bool> SendEmailAsync(string to, string subject, string body, bool isHtml = false)
    {
        return await SendEmailAsync(to, subject, body, new Dictionary<string, byte[]>());
    }

    public async Task<bool> SendEmailAsync(string to, string subject, string body, Dictionary<string, byte[]> attachments)
    {
        try
        {
            var smtpHost = configuration["Email:SmtpHost"];
            var smtpPort = int.Parse(configuration["Email:SmtpPort"] ?? "587");
            var username = configuration["Email:Username"];
            var password = configuration["Email:Password"];
            var fromName = configuration["Email:FromName"] ?? "MCQ Platform";
            var fromAddress = configuration["Email:FromAddress"];

            if (string.IsNullOrEmpty(smtpHost) || string.IsNullOrEmpty(username))
            {
                logger.LogWarning("Email not configured - skipping send to {To}", to);
                logger.LogInformation("Email would be sent to {To}: {Subject}\n{Body}", to, subject, body);
                return true;
            }

            using var client = new SmtpClient(smtpHost, smtpPort)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(username, password),
                Timeout = 30000
            };

            using var message = new MailMessage
            {
                From = new MailAddress(fromAddress!, fromName),
                Subject = subject,
                Body = body,
                IsBodyHtml = false,
                BodyEncoding = Encoding.UTF8
            };

            message.To.Add(to);

            foreach (var att in attachments)
            {
                message.Attachments.Add(new Attachment(new MemoryStream(att.Value), att.Key));
            }

            await client.SendMailAsync(message);
            logger.LogInformation("Email sent successfully to {To}: {Subject}", to, subject);
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to send email to {To}: {Subject}", to, subject);
            return false;
        }
    }

    public async Task<bool> SendWelcomeEmailAsync(string email, string fullName, string temporaryPassword)
    {
        var subject = "Welcome to MCQ Platform";
        var body = $@"Dear {fullName},

Welcome to the MCQ Platform! Your account has been created.

Your login credentials:
Email: {email}
Password: {temporaryPassword}

Please login and change your password immediately.

Best regards,
MCQ Platform Team";

        return await SendEmailAsync(email, subject, body);
    }

    public async Task<bool> SendVerificationEmailAsync(string email, string fullName, string token)
    {
        var subject = "Verify your email - MCQ Platform";
        var baseUrl = configuration["App:BaseUrl"] ?? "http://localhost:5100";
        var verificationUrl = $"{baseUrl}/verify-email?token={token}";

        var body = $@"Dear {fullName},

Thank you for registering with MCQ Platform.

Please verify your email by clicking the link below:
{verificationUrl}

If you did not create this account, please ignore this email.

Best regards,
MCQ Platform Team";

        return await SendEmailAsync(email, subject, body);
    }

    public async Task<bool> SendPasswordResetEmailAsync(string email, string fullName, string token)
    {
        var subject = "Password Reset - MCQ Platform";
        var baseUrl = configuration["App:BaseUrl"] ?? "http://localhost:5100";
        var resetUrl = $"{baseUrl}/reset-password?token={token}";

        var body = $@"Dear {fullName},

You requested a password reset. Click the link below to reset your password:
{resetUrl}

This link will expire in 24 hours.

If you did not request this, please ignore this email.

Best regards,
MCQ Platform Team";

        return await SendEmailAsync(email, subject, body);
    }

    public async Task<bool> SendEnrolmentNotificationAsync(string email, string fullName, string chapterName, string subjectName)
    {
        var subject = "You've been enrolled in a new chapter - MCQ Platform";
        var body = $@"Dear {fullName},

Good news! You have been enrolled in the chapter '{chapterName}' under subject '{subjectName}'.

You can now access any available exams for this chapter by logging into the platform.

Best regards,
MCQ Platform Team";

        return await SendEmailAsync(email, subject, body);
    }

    public async Task<bool> SendExamAvailableNotificationAsync(string email, string fullName, string examTitle, string chapterName)
    {
        var subject = $"New exam available: {examTitle} - MCQ Platform";
        var body = $@"Dear {fullName},

A new exam '{examTitle}' is now available in chapter '{chapterName}'.

Please login to take the exam before the deadline.

Best regards,
MCQ Platform Team";

        return await SendEmailAsync(email, subject, body);
    }

    public async Task<bool> SendResultReleasedNotificationAsync(string email, string fullName, string examTitle, decimal score, bool passed)
    {
        var subject = $"Exam results released: {examTitle} - MCQ Platform";
        var status = passed ? "PASSED" : "FAILED";
        var body = $@"Dear {fullName},

Your results for '{examTitle}' have been released.

Score: {score}%
Status: {status}

Please login to view your detailed results.

Best regards,
MCQ Platform Team";

        return await SendEmailAsync(email, subject, body);
    }
}