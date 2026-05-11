using MCQ3.DataConnect.Data;
using MCQ3.DataConnect.Entities;
using MCQ3.DataConnect.Requests;
using MCQ3.DataConnect.Responses;
using MCQ3.DataConnect.Utils;
using MCQ3.DataConnect.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MCQ3.DataConnect.Services;

public class UserService(AppDbContext dbContext, IEmailService emailService, ILogger<UserService> logger)
{
    private static readonly Guid TeacherRoleId = Guid.Parse("00000002-0000-0000-0000-000000000002");
    private static readonly Guid StudentRoleId = Guid.Parse("00000003-0000-0000-0000-000000000003");

    public async Task<IEnumerable<UserResponse>> GetAllAsync()
    {
        return await dbContext.Users
            .Include(u => u.RoleEntity)
            .Select(u => new UserResponse(u.Id, u.FullName, u.Email, u.RoleEntity!.Name, u.IsActive, u.TempPassword, u.CreatedAt))
            .ToListAsync();
    }

    public async Task<UserResponse?> GetByIdAsync(Guid id)
    {
        var user = await dbContext.Users.Include(u => u.RoleEntity).FirstOrDefaultAsync(u => u.Id == id);
        if (user == null) return null;
        return new UserResponse(user.Id, user.FullName, user.Email, user.RoleEntity?.Name ?? "Unknown", user.IsActive, user.TempPassword, user.CreatedAt);
    }

    public async Task<UserResponse?> CreateAsync(CreateUserRequest request)
    {
        var existingUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (existingUser != null) return null;

        var role = await dbContext.Roles.FirstOrDefaultAsync(r => r.Name == request.Role);
        if (role == null) return null;

        var tempPassword = GenerateTempPassword();
        var user = new UserAccount
        {
            FullName = request.FullName,
            Email = request.Email,
            PasswordHash = PasswordHelper.HashPassword(tempPassword),
            IsActive = true,
            TempPassword = true,
            RoleId = role.Id
        };

        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();

        try
        {
            await emailService.SendWelcomeEmailAsync(user.Email, user.FullName, tempPassword);
            logger.LogInformation("Welcome email sent to {Email}", user.Email);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Failed to send welcome email to {Email}", user.Email);
        }

        return new UserResponse(user.Id, user.FullName, user.Email, role.Name, user.IsActive, user.TempPassword, user.CreatedAt);
    }

    private static string GenerateTempPassword()
    {
        return $"Temp{PasswordHelper.GenerateSecureToken()[..8]}";
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateUserRequest request)
    {
        var user = await dbContext.Users.FindAsync(id);
        if (user == null) return false;

        if (request.FullName != null) user.FullName = request.FullName;
        if (request.IsActive.HasValue) user.IsActive = request.IsActive.Value;

        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var user = await dbContext.Users.FindAsync(id);
        if (user == null) return false;

        user.IsActive = false;
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<UserAccount?> GetUserByEmailAsync(string email)
    {
        return await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<IEnumerable<TeacherViewModel>> GetTeachersAsync()
    {
        return await dbContext.Users
            .Where(u => u.RoleId == TeacherRoleId)
            .Include(u => u.Subjects)
            .Select(u => new TeacherViewModel(
                u.Id,
                u.FullName,
                u.Email,
                u.IsActive,
                u.Subjects.Count,
                u.CreatedAt
            ))
            .ToListAsync();
    }

    public async Task<TeacherViewModel?> CreateTeacherAsync(CreateTeacherRequest request, Guid createdById)
    {
        var existingUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (existingUser != null) return null;

        var tempPassword = GenerateTempPassword();
        var user = new UserAccount
        {
            FullName = request.FullName,
            Email = request.Email,
            PasswordHash = PasswordHelper.HashPassword(tempPassword),
            IsActive = true,
            TempPassword = true,
            CreatedById = createdById,
            RoleId = TeacherRoleId
        };

        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();

        try
        {
            await emailService.SendWelcomeEmailAsync(user.Email, user.FullName, tempPassword);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Failed to send welcome email to {Email}", user.Email);
        }

        return new TeacherViewModel(user.Id, user.FullName, user.Email, user.IsActive, 0, user.CreatedAt);
    }

    public async Task<bool> UpdateTeacherAsync(Guid id, UpdateTeacherRequest request)
    {
        var user = await dbContext.Users.FindAsync(id);
        if (user == null || user.RoleId != TeacherRoleId) return false;

        if (request.FullName != null) user.FullName = request.FullName;
        if (request.IsActive.HasValue) user.IsActive = request.IsActive.Value;

        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<StudentViewModel>> GetStudentsAsync()
    {
        return await dbContext.Users
            .Where(u => u.RoleId == StudentRoleId)
            .Select(u => new StudentViewModel(
                u.Id,
                u.FullName,
                u.Email,
                u.IsActive
            ))
            .ToListAsync();
    }

    public async Task<bool> AssignStudentToChapterAsync(Guid studentId, Guid chapterId, Guid assignedById)
    {
        var student = await dbContext.Users.FindAsync(studentId);
        if (student == null || student.RoleId != StudentRoleId) return false;

        var chapter = await dbContext.Chapters.FindAsync(chapterId);
        if (chapter == null) return false;

        var teacher = await dbContext.Users
            .Include(u => u.Subjects)
                .ThenInclude(s => s.Chapters)
            .FirstOrDefaultAsync(u => u.Id == assignedById && u.RoleId == TeacherRoleId);

        var isTeacherChapter = teacher?.Subjects
            .SelectMany(s => s.Chapters)
            .Any(c => c.Id == chapterId) ?? false;

        if (!isTeacherChapter) return false;

        var existing = await dbContext.StudentChapters
            .FirstOrDefaultAsync(sa => sa.StudentId == studentId && sa.ChapterId == chapterId && sa.IsActive);

        if (existing != null) return true;

        var assignment = new StudentChapter
        {
            StudentId = studentId,
            ChapterId = chapterId,
            AssignedById = assignedById,
            IsActive = true
        };

        dbContext.StudentChapters.Add(assignment);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> RemoveStudentFromChapterAsync(Guid studentId, Guid chapterId, Guid teacherId)
    {
        var teacher = await dbContext.Users
            .Include(u => u.Subjects)
                .ThenInclude(s => s.Chapters)
            .FirstOrDefaultAsync(u => u.Id == teacherId && u.RoleId == TeacherRoleId);

        var isTeacherChapter = teacher?.Subjects
            .SelectMany(s => s.Chapters)
            .Any(c => c.Id == chapterId) ?? false;

        if (!isTeacherChapter) return false;

        var hasActivity = await dbContext.Attempts
            .AnyAsync(a => a.StudentId == studentId && a.Exam.ChapterId == chapterId && a.SubmittedAt != null);

        if (hasActivity)
        {
            var assignment = await dbContext.StudentChapters
                .FirstOrDefaultAsync(sa => sa.StudentId == studentId && sa.ChapterId == chapterId && sa.IsActive);
            if (assignment != null)
            {
                assignment.IsActive = false;
                await dbContext.SaveChangesAsync();
            }
            return true;
        }

        var assignmentToRemove = await dbContext.StudentChapters
            .FirstOrDefaultAsync(sa => sa.StudentId == studentId && sa.ChapterId == chapterId);

        if (assignmentToRemove != null)
        {
            dbContext.StudentChapters.Remove(assignmentToRemove);
            await dbContext.SaveChangesAsync();
        }
        return true;
    }

    public async Task<IEnumerable<StudentChapterViewModel>> GetStudentAssignmentsAsync(Guid chapterId, Guid teacherId)
    {
        var teacher = await dbContext.Users
            .Include(u => u.Subjects)
                .ThenInclude(s => s.Chapters)
            .FirstOrDefaultAsync(u => u.Id == teacherId && u.RoleId == TeacherRoleId);

        var isTeacherChapter = teacher?.Subjects
            .SelectMany(s => s.Chapters)
            .Any(c => c.Id == chapterId) ?? false;

        if (!isTeacherChapter) return Enumerable.Empty<StudentChapterViewModel>();

        return await dbContext.StudentChapters
            .Where(sa => sa.ChapterId == chapterId && sa.IsActive)
            .Include(sa => sa.Student)
            .Select(sa => new StudentChapterViewModel(
                sa.Id,
                sa.StudentId,
                sa.Student.FullName,
                sa.Student.Email,
                sa.AssignedById,
                sa.CreatedAt
            ))
            .ToListAsync();
    }
}