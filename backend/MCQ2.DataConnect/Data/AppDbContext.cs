using MCQ3.DataConnect.Entities;
using Microsoft.EntityFrameworkCore;

namespace MCQ3.DataConnect.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<UserAccount> Users => Set<UserAccount>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Teacher> Teachers => Set<Teacher>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<EmailVerification> EmailVerifications => Set<EmailVerification>();
    public DbSet<PasswordReset> PasswordResets => Set<PasswordReset>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<Chapter> Chapters => Set<Chapter>();
    public DbSet<Question> Questions => Set<Question>();
    public DbSet<AnswerOption> AnswerOptions => Set<AnswerOption>();
    public DbSet<Enrolment> Enrolments => Set<Enrolment>();
    public DbSet<Exam> Exams => Set<Exam>();
    public DbSet<ExamQuestion> ExamQuestions => Set<ExamQuestion>();
    public DbSet<Attempt> Attempts => Set<Attempt>();
    public DbSet<AttemptAnswer> AttemptAnswers => Set<AttemptAnswer>();
    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    public DbSet<PlatformSetting> PlatformSettings => Set<PlatformSetting>();
    public DbSet<StudentChapter> StudentChapters => Set<StudentChapter>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var schemaConfiguration = new SchemaConfiguration(builder);
        schemaConfiguration.Configure();

        var seedConfiguration = new SeedConfiguration(builder);
        seedConfiguration.Configure();
    }
   
}