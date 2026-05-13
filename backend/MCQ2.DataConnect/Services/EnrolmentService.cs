using System.Globalization;
using CsvHelper;
using MCQ3.DataConnect.Data;
using MCQ3.DataConnect.Entities;
using MCQ3.DataConnect.Requests;
using MCQ3.DataConnect.Utils;
using MCQ3.DataConnect.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MCQ3.DataConnect.Services;

public class EnrolmentService(AppDbContext dbContext, IEmailService emailService, ILogger<EnrolmentService> logger)
{
    private static readonly Guid StudentRoleId = Guid.Parse("00000003-0000-0000-0000-000000000003");

    public async Task<IEnumerable<EnrolmentViewModel>> GetByChapterAsync(Guid chapterId)
    {
        return await dbContext.Enrolments
            .Where(e => e.ChapterId == chapterId && e.RemovedAt == null && e.ExamId == null)
            .Select(e => new EnrolmentViewModel(
                e.Id, e.StudentId!.Value, e.Student!.FullName, e.Student.Email,
                e.ChapterId, e.Chapter.Title, e.ExamId, e.Exam == null ? null : e.Exam.Title,
                e.EnrolledById, e.EnrolledBy!.FullName,
                e.EnrolledAt, e.ExpiresAt, e.RemovedAt
            )).ToListAsync();
    }

    public async Task<IEnumerable<EnrolmentViewModel>> GetByExamAsync(Guid examId)
    {
        return await dbContext.Enrolments
            .Where(e => e.ExamId == examId && e.RemovedAt == null)
            .Select(e => new EnrolmentViewModel(
                e.Id, e.StudentId!.Value, e.Student!.FullName, e.Student.Email,
                e.ChapterId, e.Chapter!.Title, e.ExamId, e.Exam == null ? null : e.Exam.Title,
                e.EnrolledById, e.EnrolledBy!.FullName,
                e.EnrolledAt, e.ExpiresAt, e.RemovedAt
            )).ToListAsync();
    }

    public async Task<IEnumerable<EnrolmentViewModel>> GetMyEnrolmentsAsync(Guid studentId)
    {
        var now = DateTime.UtcNow;
        return await dbContext.Enrolments
            .Include(e => e.Chapter)
                .ThenInclude(c => c!.Subject)
            .Where(e => e.StudentId == studentId && e.RemovedAt == null && (e.ExpiresAt == null || e.ExpiresAt > now))
            .Select(e => new EnrolmentViewModel(
                e.Id, e.StudentId!.Value, e.Student!.FullName, e.Student.Email,
                e.ChapterId, e.Chapter!.Title, e.ExamId, e.Exam == null ? null : e.Exam.Title,
                e.EnrolledById, e.EnrolledBy!.FullName,
                e.EnrolledAt, e.ExpiresAt, e.RemovedAt
            )).ToListAsync();
    }

    public async Task<IEnumerable<object>> GetStudentsForExamEnrolmentAsync(Guid examId)
    {
        var exam = await dbContext.Exams
            .Include(e => e.Chapter)
            .FirstOrDefaultAsync(e => e.Id == examId);
        
        if (exam == null) return Enumerable.Empty<object>();

        var now = DateTime.UtcNow;
        var enrolledStudentIds = await dbContext.Enrolments
            .Where(e => e.ExamId == examId && e.RemovedAt == null)
            .Select(e => e.StudentId)
            .ToListAsync();

        var availableStudents = await dbContext.StudentChapters
            .Include(sc => sc.Student)
            .Where(sc => sc.ChapterId == exam.ChapterId && sc.IsActive == true && sc.Student != null && sc.Student.IsActive == true)
            .Where(sc => !enrolledStudentIds.Contains(sc.StudentId))
            .Select(sc => new
            {
                studentId = sc.StudentId,
                studentName = sc.Student!.FullName,
                studentEmail = sc.Student.Email
            })
            .ToListAsync();

        return availableStudents;
    }

    public async Task<int> EnrolStudentsInExamAsync(Guid examId, List<Guid> studentIds, Guid teacherId, DateTime? expiresAt = null)
    {
        var exam = await dbContext.Exams
            .Include(e => e.Chapter)
                .ThenInclude(c => c!.Subject)
            .FirstOrDefaultAsync(e => e.Id == examId);

        if (exam == null) return 0;

        var enrolledCount = 0;
        foreach (var studentId in studentIds)
        {
            var existingActive = await dbContext.Enrolments
                .FirstOrDefaultAsync(e => e.StudentId == studentId && e.ExamId == examId && e.RemovedAt == null);

            if (existingActive != null) continue;

            var existingRemoved = await dbContext.Enrolments
                .FirstOrDefaultAsync(e => e.StudentId == studentId && e.ChapterId == exam.ChapterId && e.RemovedAt != null);

            var student = await dbContext.Users.FindAsync(studentId);
            if (student == null) continue;

            if (existingRemoved != null)
            {
                existingRemoved.ExamId = examId;
                existingRemoved.RemovedAt = null;
                existingRemoved.EnrolledById = teacherId;
                existingRemoved.EnrolledAt = DateTime.UtcNow;
                existingRemoved.ExpiresAt = expiresAt;
                enrolledCount++;
            }
            else
            {
                var enrolment = new Enrolment
                {
                    StudentId = studentId,
                    ChapterId = exam.ChapterId,
                    ExamId = examId,
                    EnrolledById = teacherId,
                    ExpiresAt = expiresAt
                };

                dbContext.Enrolments.Add(enrolment);
                enrolledCount++;
            }

            try
            {
                await emailService.SendEnrolmentNotificationAsync(student.Email, student.FullName, exam.Title, exam.Chapter.Subject.Title);
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Failed to send enrolment notification to {Email}", student.Email);
            }
        }

        if (enrolledCount > 0)
        {
            await dbContext.SaveChangesAsync();
        }

        return enrolledCount;
    }

    public async Task<bool> EnrolAsync(Guid chapterId, Guid studentId, Guid teacherId, DateTime? expiresAt = null)
    {
        var existing = await dbContext.Enrolments
            .FirstOrDefaultAsync(e => e.StudentId == studentId && e.ChapterId == chapterId && e.RemovedAt == null);

        if (existing != null) return false;

        var chapter = await dbContext.Chapters.Include(c => c.Subject).FirstOrDefaultAsync(c => c.Id == chapterId);
        var student = await dbContext.Users.FindAsync(studentId);

        var enrolment = new Enrolment
        {
            StudentId = studentId,
            ChapterId = chapterId,
            EnrolledById = teacherId,
            ExpiresAt = expiresAt
        };

        dbContext.Enrolments.Add(enrolment);
        await dbContext.SaveChangesAsync();

        if (chapter != null && student != null)
        {
            try
            {
                await emailService.SendEnrolmentNotificationAsync(student.Email, student.FullName, chapter.Title, chapter.Subject.Title);
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Failed to send enrolment notification to {Email}", student.Email);
            }
        }

        return true;
    }

    public async Task<EnrolmentViewModel?> RegisterAndEnrolAsync(Guid chapterId, RegisterStudentRequest request, Guid teacherId)
    {
        var chapter = await dbContext.Chapters.Include(c => c.Subject).FirstOrDefaultAsync(c => c.Id == chapterId);
        if (chapter == null) return null;

        var existingUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        UserAccount student;
        var isNewStudent = false;
        var tempPassword = "";

        if (existingUser != null)
        {
            var existingEnrolment = await dbContext.Enrolments
                .FirstOrDefaultAsync(e => e.StudentId == existingUser.Id && e.ChapterId == chapterId && e.RemovedAt == null);
            if (existingEnrolment != null) return null;
            student = existingUser;
        }
        else
        {
            isNewStudent = true;
            tempPassword = $"Temp{PasswordHelper.GenerateSecureToken()[..8]}";
            student = new UserAccount
            {
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = PasswordHelper.HashPassword(tempPassword),
                RoleId = StudentRoleId,
                IsActive = true,
                TempPassword = true
            };
            dbContext.Users.Add(student);
            await dbContext.SaveChangesAsync();
        }

        var enrolment = new Enrolment
        {
            StudentId = student.Id,
            ChapterId = chapterId,
            EnrolledById = teacherId
        };

        dbContext.Enrolments.Add(enrolment);
        await dbContext.SaveChangesAsync();

        try
        {
            if (isNewStudent)
            {
                await emailService.SendWelcomeEmailAsync(student.Email, student.FullName, tempPassword);
            }
            await emailService.SendEnrolmentNotificationAsync(student.Email, student.FullName, chapter.Title, chapter.Subject.Title);
            logger.LogInformation("Enrolment notification sent to {Email}", student.Email);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Failed to send enrolment notification to {Email}", student.Email);
        }

        return new EnrolmentViewModel(
            enrolment.Id, student.Id, student.FullName, student.Email,
            chapterId, chapter.Title, null, null, teacherId, "", enrolment.EnrolledAt, null, null
        );
    }

    public async Task<BulkEnrolResult> BulkEnrolAsync(Guid chapterId, Guid teacherId, Stream csvStream)
    {
        using var reader = new StreamReader(csvStream);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        csv.Read();
        csv.ReadHeader();
        var emails = new List<string>();
        while (csv.Read())
        {
            var email = csv.GetField<string>("Email")?.Trim().ToLower();
            if (!string.IsNullOrWhiteSpace(email))
                emails.Add(email);
        }

        var enrolled = new List<string>();
        var skipped = new List<string>();
        var unmatched = new List<string>();

        foreach (var email in emails.Distinct())
        {
            var student = await dbContext.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email);
            if (student == null)
            {
                unmatched.Add(email);
                continue;
            }

            var existing = await dbContext.Enrolments
                .FirstOrDefaultAsync(e => e.StudentId == student.Id && e.ChapterId == chapterId && e.RemovedAt == null);
            if (existing != null)
            {
                skipped.Add(email);
                continue;
            }

            var enrolment = new Enrolment
            {
                StudentId = student.Id,
                ChapterId = chapterId,
                EnrolledById = teacherId
            };
            dbContext.Enrolments.Add(enrolment);
            enrolled.Add(email);
        }

        await dbContext.SaveChangesAsync();
        return new BulkEnrolResult(enrolled, skipped, unmatched);
    }

    public async Task<bool> RemoveAsync(Guid chapterId, Guid studentId)
    {
        var enrolment = await dbContext.Enrolments
            .FirstOrDefaultAsync(e => e.StudentId == studentId && e.ChapterId == chapterId && e.RemovedAt == null);
        if (enrolment == null) return false;

        enrolment.RemovedAt = DateTime.UtcNow;
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ReEnrolAsync(Guid chapterId, Guid studentId, Guid teacherId)
    {
        var existing = await dbContext.Enrolments
            .FirstOrDefaultAsync(e => e.StudentId == studentId && e.ChapterId == chapterId && e.RemovedAt != null);
        if (existing == null) return false;

        var newEnrolment = new Enrolment
        {
            StudentId = studentId,
            ChapterId = chapterId,
            EnrolledById = teacherId,
            TransferredFromChapterId = chapterId
        };

        dbContext.Enrolments.Add(newEnrolment);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateAsync(Guid chapterId, Guid studentId, UpdateEnrolmentRequest request)
    {
        var enrolment = await dbContext.Enrolments
            .FirstOrDefaultAsync(e => e.StudentId == studentId && e.ChapterId == chapterId && e.RemovedAt == null);
        if (enrolment == null) return false;

        if (request.ExpiresAt.HasValue) enrolment.ExpiresAt = request.ExpiresAt;
        if (request.TransferToChapterId.HasValue)
        {
            var newEnrolment = new Enrolment
            {
                StudentId = studentId,
                ChapterId = request.TransferToChapterId.Value,
                EnrolledById = enrolment.EnrolledById,
                TransferredFromChapterId = chapterId
            };
            dbContext.Enrolments.Add(newEnrolment);
            enrolment.RemovedAt = DateTime.UtcNow;
        }

        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<int> UnenrollStudentsFromExamAsync(Guid examId, List<Guid> studentIds)
    {
        var enrolments = await dbContext.Enrolments
            .Where(e => e.ExamId == examId && studentIds.Contains(e.StudentId!.Value) && e.RemovedAt == null)
            .ToListAsync();

        foreach (var enrolment in enrolments)
        {
            enrolment.RemovedAt = DateTime.UtcNow;
        }

        await dbContext.SaveChangesAsync();
        return enrolments.Count;
    }
}