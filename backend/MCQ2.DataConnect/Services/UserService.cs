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
            .Select(u => new UserResponse(u.Id, u.FullName, u.Username, u.Email, u.RoleEntity!.Name, u.IsActive, u.TempPassword, u.CreatedAt))
            .ToListAsync();
    }

    public async Task<UserResponse?> GetByIdAsync(Guid id)
    {
        var user = await dbContext.Users.Include(u => u.RoleEntity).FirstOrDefaultAsync(u => u.Id == id);
        if (user == null) return null;
        return new UserResponse(user.Id, user.FullName, user.Username, user.Email, user.RoleEntity?.Name ?? "Unknown", user.IsActive, user.TempPassword, user.CreatedAt);
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
            Username = request.Email.Split('@')[0] + Guid.NewGuid().ToString("N")[..6],
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

        return new UserResponse(user.Id, user.FullName, user.Username, user.Email, role.Name, user.IsActive, user.TempPassword, user.CreatedAt);
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
        if (!string.IsNullOrEmpty(request.Password))
        {
            user.PasswordHash = PasswordHelper.HashPassword(request.Password);
            user.TempPassword = false;
        }

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
             .Include(u => u.Teacher)
             .Include(u => u.Subjects)
             .Select(u => new TeacherViewModel(
                 u.Id,
                 u.FullName,
                 u.Email,
                 u.IsActive,
                 u.Subjects.Count,
                 u.CreatedAt,
                 u.Teacher != null ? u.Teacher.Title : null,
                 u.Teacher != null ? u.Teacher.ContactNo : null,
                 u.Teacher != null ? u.Teacher.Address : null,
                 u.Teacher != null ? u.Teacher.NID : null
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
            Username = request.Email.Split('@')[0] + Guid.NewGuid().ToString("N")[..6],
            PasswordHash = PasswordHelper.HashPassword(tempPassword),
            IsActive = true,
            TempPassword = true,
            CreatedById = createdById,
            RoleId = TeacherRoleId
        };

        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();

var teacher = new Teacher
         {
             Name = request.FullName,
             Email = request.Email,
             Title = request.Title ?? string.Empty,
             ContactNo = request.Phone ?? string.Empty,
             Address = request.Address ?? string.Empty,
             NID = request.NID ?? string.Empty,
             UserId = user.Id,
             IsActive = true
         };

        dbContext.Teachers.Add(teacher);
        await dbContext.SaveChangesAsync();

        try
        {
            await emailService.SendWelcomeEmailAsync(user.Email, user.FullName, tempPassword);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Failed to send welcome email to {Email}", user.Email);
        }

        return new TeacherViewModel(user.Id, user.FullName, user.Email, user.IsActive, 0, user.CreatedAt, request.Title, request.Phone, request.Address, request.NID);
    }

    public async Task<bool> UpdateTeacherAsync(Guid id, UpdateTeacherRequest request)
    {
        var user = await dbContext.Users.FindAsync(id);
        if (user == null || user.RoleId != TeacherRoleId) return false;

        if (request.FullName != null) user.FullName = request.FullName;
        if (request.IsActive.HasValue) user.IsActive = request.IsActive.Value;

        var teacher = await dbContext.Teachers.FirstOrDefaultAsync(t => t.UserId == id);
        if (teacher != null)
        {
            if (request.Title != null) teacher.Title = request.Title;
            if (request.Phone != null) teacher.ContactNo = request.Phone;
            if (request.Address != null) teacher.Address = request.Address;
            if (request.NID != null) teacher.NID = request.NID;
            if (request.FullName != null) teacher.Name = request.FullName;
        }

        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<StudentViewModel>> GetStudentsAsync()
    {
return await dbContext.Users
             .Where(u => u.RoleId == StudentRoleId)
             .Include(u => u.Student)
             .Select(u => new StudentViewModel(
                 u.Id,
                 u.FullName,
                 u.Email,
                 u.IsActive,
                 u.CreatedAt,
                 u.Student != null ? u.Student.Code : null,
                 u.Student != null ? u.Student.NID : null,
                 u.Student != null ? u.Student.Address : null,
                 u.Student != null ? u.Student.ContactNo : null,
                 u.Student != null ? u.Student.FatherName : null,
                 u.Student != null ? u.Student.FatherContact : null,
                 u.Student != null ? u.Student.MotherName : null,
                 u.Student != null ? u.Student.MotherContact : null,
                 u.Username
             ))
             .ToListAsync();
    }

    public async Task<StudentViewModel?> CreateStudentAsync(CreateStudentRequest request, Guid createdById)
    {
        var existingUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (existingUser != null) return null;

        var studentUsername = !string.IsNullOrWhiteSpace(request.Code) ? request.Code : $"STU{Guid.NewGuid().ToString("N")[..6]}";

        var tempPassword = GenerateTempPassword();
        var user = new UserAccount
        {
            FullName = request.FullName,
            Email = request.Email,
            Username = studentUsername,
            PasswordHash = PasswordHelper.HashPassword(tempPassword),
            IsActive = true,
            TempPassword = true,
            CreatedById = createdById,
            RoleId = StudentRoleId
        };

        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();

        var student = new Student
        {
            Name = request.FullName,
            Email = request.Email,
            Code = request.Code ?? string.Empty,
            NID = request.NID ?? string.Empty,
            Address = request.Address ?? string.Empty,
            ContactNo = request.Phone ?? string.Empty,
            FatherName = request.FatherName ?? string.Empty,
            FatherContact = request.FatherContact ?? string.Empty,
            MotherName = request.MotherName ?? string.Empty,
            MotherContact = request.MotherContact ?? string.Empty,
            UserId = user.Id,
            IsActive = true
        };

        dbContext.Students.Add(student);
        await dbContext.SaveChangesAsync();

        try
        {
            await emailService.SendWelcomeEmailAsync(user.Email, user.FullName, tempPassword);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Failed to send welcome email to {Email}", user.Email);
        }

        return new StudentViewModel(
            user.Id, user.FullName, user.Email, user.IsActive, user.CreatedAt,
            request.Code, request.NID, request.Address, request.Phone,
            request.FatherName, request.FatherContact, request.MotherName, request.MotherContact,
            studentUsername
        );
    }

    public async Task<bool> UpdateStudentAsync(Guid id, UpdateStudentRequest request)
    {
        var user = await dbContext.Users.FindAsync(id);
        if (user == null || user.RoleId != StudentRoleId) return false;

        if (request.FullName != null) user.FullName = request.FullName;
        if (request.IsActive.HasValue) user.IsActive = request.IsActive.Value;

        var student = await dbContext.Students.FirstOrDefaultAsync(s => s.UserId == id);
        if (student != null)
        {
            if (request.Code != null) student.Code = request.Code;
            if (request.NID != null) student.NID = request.NID;
            if (request.Address != null) student.Address = request.Address;
            if (request.Phone != null) student.ContactNo = request.Phone;
            if (request.FatherName != null) student.FatherName = request.FatherName;
            if (request.FatherContact != null) student.FatherContact = request.FatherContact;
            if (request.MotherName != null) student.MotherName = request.MotherName;
            if (request.MotherContact != null) student.MotherContact = request.MotherContact;
            if (request.FullName != null) student.Name = request.FullName;
        }

        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteStudentAsync(Guid id)
    {
        var user = await dbContext.Users.FindAsync(id);
        if (user == null || user.RoleId != StudentRoleId) return false;

        user.IsActive = false;
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> AssignStudentToChapterAsync(Guid studentId, Guid chapterId, Guid assignedById)
    {
        var student = await dbContext.Users.FindAsync(studentId);
        if (student == null || student.RoleId != StudentRoleId) return false;

        var chapter = await dbContext.Chapters.FindAsync(chapterId);
        if (chapter == null) return false;

        var teacher = await dbContext.Teachers
            .Include(t => t.Subjects)
                .ThenInclude(s => s.Chapters)
            .FirstOrDefaultAsync(t => t.UserId == assignedById);

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
        var teacher = await dbContext.Teachers
            .Include(t => t.Subjects)
                .ThenInclude(s => s.Chapters)
            .FirstOrDefaultAsync(t => t.UserId == teacherId);

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
        var teacher = await dbContext.Teachers
            .Include(t => t.Subjects)
                .ThenInclude(s => s.Chapters)
            .FirstOrDefaultAsync(t => t.UserId == teacherId);

        var isTeacherChapter = teacher?.Subjects
            .SelectMany(s => s.Chapters)
            .Any(c => c.Id == chapterId) ?? false;

        if (!isTeacherChapter) return Enumerable.Empty<StudentChapterViewModel>();

        return await dbContext.StudentChapters
            .Where(sa => sa.ChapterId == chapterId && sa.IsActive && sa.StudentId != null)
            .Include(sa => sa.Student)
            .Select(sa => new StudentChapterViewModel(
                sa.Id,
                sa.StudentId!.Value,
                sa.Student!.FullName,
                sa.Student.Email,
                sa.AssignedById,
                sa.CreatedAt
            ))
            .ToListAsync();
    }
}