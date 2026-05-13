using Microsoft.Extensions.DependencyInjection;

namespace MCQ3.DataConnect.Services;

public static class ServiceExtension
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<AuthService>();
        services.AddScoped<UserService>();
        services.AddScoped<SubjectService>();
        services.AddScoped<ChapterService>();
        services.AddScoped<QuestionService>();
        services.AddScoped<ExamService>();
        services.AddScoped<EnrolmentService>();
        services.AddScoped<AttemptService>();
        services.AddScoped<UploadService>();
        services.AddScoped<AnalyticsService>();
        services.AddScoped<NotificationService>();
        services.AddScoped<AuditLogService>();
        return services;
    }
}