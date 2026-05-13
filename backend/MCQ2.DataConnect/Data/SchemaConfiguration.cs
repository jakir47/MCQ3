using MCQ3.DataConnect.Entities;
using Microsoft.EntityFrameworkCore;

namespace MCQ3.DataConnect.Data;
public class SchemaConfiguration(ModelBuilder builder)
{
    public void Configure()
    {
        builder.Entity<Role>(e =>
        {
            e.ToTable("Role");
            e.HasIndex(r => r.Name).IsUnique();
        });

        builder.Entity<Teacher>(e =>
        {
            e.ToTable("Teacher");
            e.HasKey(t => t.UserId);
            e.HasIndex(t => t.Email).IsUnique();
            e.HasIndex(t => t.NID).IsUnique();
            e.HasOne(t => t.User).WithOne(u => u.Teacher)
             .HasForeignKey<Teacher>(t => t.UserId).OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Student>(e =>
        {
            e.ToTable("Student");
            e.HasKey(s => s.UserId);
            e.HasIndex(s => s.Code).IsUnique();
            e.HasIndex(s => s.Email).IsUnique();
            e.HasIndex(s => s.NID).IsUnique();
            e.HasOne(s => s.User).WithOne(u => u.Student)
             .HasForeignKey<Student>(s => s.UserId).OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<UserAccount>(e =>
        {
            e.ToTable("UserAccount");
            e.HasIndex(u => u.Email).IsUnique();
            e.HasIndex(u => u.Username).IsUnique();
            e.HasOne(u => u.CreatedBy).WithMany()
             .HasForeignKey(u => u.CreatedById).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(u => u.RoleEntity).WithMany(r => r.Users)
             .HasForeignKey(u => u.RoleId).OnDelete(DeleteBehavior.SetNull);
        });

        builder.Entity<Subject>(e =>
        {
            e.ToTable("Subject");
            e.HasOne(s => s.Teacher).WithMany(t => t.Subjects)
             .HasForeignKey(s => s.TeacherId).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<Chapter>(e =>
        {
            e.ToTable("Chapter");
            e.HasOne(c => c.Subject).WithMany(s => s.Chapters)
             .HasForeignKey(c => c.SubjectId).OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Question>(e =>
        {
            e.ToTable("Question");
            e.Property(q => q.PositiveMarks).HasColumnType("decimal(5,2)");
            e.Property(q => q.NegativeMarks).HasColumnType("decimal(5,2)");
            e.Property(q => q.Difficulty).HasConversion<string>().IsRequired(false);
            e.HasOne(q => q.Chapter).WithMany(c => c.Questions)
             .HasForeignKey(q => q.ChapterId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(q => q.SourceQuestion).WithMany()
             .HasForeignKey(q => q.SourceQuestionId).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<AnswerOption>(e =>
        {
            e.ToTable("AnswerOption");
            e.HasOne(a => a.Question).WithMany(q => q.AnswerOptions)
             .HasForeignKey(a => a.QuestionId).OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Enrolment>(e =>
        {
            e.ToTable("Enrolment");
            e.HasIndex(en => new { en.StudentId, en.ChapterId }).IsUnique();
            e.HasOne(en => en.Student).WithMany(u => u.Enrolments)
             .HasForeignKey(en => en.StudentId).OnDelete(DeleteBehavior.SetNull);
            e.HasOne(en => en.Chapter).WithMany(c => c.Enrolments)
             .HasForeignKey(en => en.ChapterId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(en => en.EnrolledBy).WithMany()
             .HasForeignKey(en => en.EnrolledById).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<Exam>(e =>
        {
            e.ToTable("Exam");
            e.Property(ex => ex.TotalMarks).HasColumnType("decimal(7,2)");
            e.Property(ex => ex.PassingScore).HasColumnType("decimal(7,2)");
            e.Property(ex => ex.Status).HasConversion<string>();
            e.HasOne(ex => ex.Chapter).WithMany(c => c.Exams)
             .HasForeignKey(ex => ex.ChapterId).OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<ExamQuestion>(e =>
        {
            e.ToTable("ExamQuestion");
            e.HasOne(eq => eq.Exam).WithMany(ex => ex.ExamQuestions)
             .HasForeignKey(eq => eq.ExamId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(eq => eq.Question).WithMany(q => q.ExamQuestions)
             .HasForeignKey(eq => eq.QuestionId).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<Attempt>(e =>
        {
            e.ToTable("Attempt");
            e.Property(a => a.Score).HasColumnType("decimal(7,2)");
            e.HasIndex(a => new { a.ExamId, a.StudentId, a.AttemptNumber }).IsUnique();
            e.HasOne(a => a.Exam).WithMany(ex => ex.Attempts)
             .HasForeignKey(a => a.ExamId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(a => a.Student).WithMany(u => u.Attempts)
             .HasForeignKey(a => a.StudentId).OnDelete(DeleteBehavior.SetNull);
        });

        builder.Entity<AttemptAnswer>(e =>
        {
            e.ToTable("AttemptAnswer");
            e.Property(aa => aa.MarksAwarded).HasColumnType("decimal(5,2)");
            e.HasOne(aa => aa.Attempt).WithMany(a => a.AttemptAnswers)
             .HasForeignKey(aa => aa.AttemptId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(aa => aa.Question).WithMany(q => q.AttemptAnswers)
             .HasForeignKey(aa => aa.QuestionId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(aa => aa.SelectedOption).WithMany(o => o.AttemptAnswers)
             .HasForeignKey(aa => aa.SelectedOptionId).OnDelete(DeleteBehavior.SetNull);
        });

        builder.Entity<AuditLog>(e =>
        {
            e.ToTable("AuditLog");
            e.HasOne(al => al.Actor).WithMany()
             .HasForeignKey(al => al.ActorId).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<StudentChapter>(e =>
        {
            e.ToTable("StudentChapter");
            e.HasIndex(sc => new { sc.StudentId, sc.ChapterId }).IsUnique();
            e.HasOne(sc => sc.Student).WithMany(s => s.StudentChapters)
             .HasForeignKey(sc => sc.StudentId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(sc => sc.Chapter).WithMany()
             .HasForeignKey(sc => sc.ChapterId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(sc => sc.AssignedBy).WithMany()
             .HasForeignKey(sc => sc.AssignedById).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<RefreshToken>(e => e.ToTable("RefreshToken"));
        builder.Entity<EmailVerification>(e => e.ToTable("EmailVerification"));
        builder.Entity<PasswordReset>(e => e.ToTable("PasswordReset"));
        builder.Entity<Notification>(e => e.ToTable("Notification"));
    }
}