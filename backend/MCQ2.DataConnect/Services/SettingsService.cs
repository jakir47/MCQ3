using MCQ3.DataConnect.Data;
using MCQ3.DataConnect.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MCQ3.DataConnect.Services;

public class SettingsService(AppDbContext dbContext, IConfiguration configuration, ILogger<SettingsService> logger)
{
    private static readonly Dictionary<string, (string DefaultValue, string Category, string Description)> DefaultSettings = new()
    {
        { "SiteName", ("MCQ Platform", "General", "Platform name") },
        { "SiteLogo", ("", "General", "Platform logo URL") },
        { "AllowRegistration", ("true", "General", "Allow self-registration") },
        { "SessionTimeoutMinutes", ("60", "Security", "Session timeout in minutes") },
        { "MaxLoginAttempts", ("5", "Security", "Maximum failed login attempts before lockout") },
        { "LockoutMinutes", ("15", "Security", "Lockout duration in minutes") },
        { "SmtpHost", ("", "Email", "SMTP server host") },
        { "SmtpPort", ("587", "Email", "SMTP server port") },
        { "SmtpUsername", ("", "Email", "SMTP username") },
        { "SmtpPassword", ("", "Email", "SMTP password") },
        { "EmailFromName", ("MCQ Platform", "Email", "Email sender name") },
        { "EmailFromAddress", ("noreply@localhost", "Email", "Email sender address") },
        { "EnableEnrolmentNotifications", ("true", "Notifications", "Send enrolment notifications") },
        { "EnableExamNotifications", ("true", "Notifications", "Send exam availability notifications") },
        { "EnableResultNotifications", ("true", "Notifications", "Send result release notifications") },
        { "ExpiryReminderDays", ("7", "Notifications", "Days before expiry to send reminder") },
        { "ExamClosingReminderHours", ("24", "Notifications", "Hours before exam closes to send reminder") },
        { "MaxImageSizeMB", ("5", "Media", "Maximum image upload size in MB") },
        { "MaxAudioSizeMB", ("25", "Media", "Maximum audio upload size in MB") },
        { "AllowedImageTypes", ("image/jpeg,image/png,image/webp", "Media", "Allowed image MIME types") },
        { "AllowedAudioTypes", ("audio/mpeg,audio/wav,audio/ogg", "Media", "Allowed audio MIME types") },
    };

    public async Task InitializeDefaultsAsync()
    {
        var existingKeys = await dbContext.PlatformSettings.Select(s => s.Key).ToListAsync();
        var missing = DefaultSettings.Keys.Where(k => !existingKeys.Contains(k)).ToList();

        foreach (var key in missing)
        {
            var (defaultValue, category, description) = DefaultSettings[key];
            var configValue = configuration[$"App:{key}"] ?? configuration[$"NotificationSettings:{key}"] ?? configuration[$"FileStorage:{key}"];

            dbContext.PlatformSettings.Add(new PlatformSetting
            {
                Key = key,
                Value = configValue ?? defaultValue,
                Category = category,
                Description = description
            });
        }

        if (missing.Any())
        {
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Initialized {Count} default platform settings", missing.Count);
        }
    }

    public async Task<Dictionary<string, string>> GetAllAsync()
    {
        await InitializeDefaultsAsync();
        return await dbContext.PlatformSettings.ToDictionaryAsync(s => s.Key, s => s.Value);
    }

    public async Task<string?> GetAsync(string key)
    {
        var setting = await dbContext.PlatformSettings.FirstOrDefaultAsync(s => s.Key == key);
        return setting?.Value ?? DefaultSettings.GetValueOrDefault(key).DefaultValue;
    }

    public async Task<bool> SetAsync(string key, string value)
    {
        var setting = await dbContext.PlatformSettings.FirstOrDefaultAsync(s => s.Key == key);
        if (setting == null)
        {
            if (!DefaultSettings.ContainsKey(key)) return false;
            var (defaultValue, category, description) = DefaultSettings[key];
            setting = new PlatformSetting { Key = key, Value = value, Category = category, Description = description };
            dbContext.PlatformSettings.Add(setting);
        }
        else
        {
            setting.Value = value;
        }
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> SetManyAsync(Dictionary<string, string> settings)
    {
        foreach (var (key, value) in settings)
        {
            await SetAsync(key, value);
        }
        return true;
    }
}