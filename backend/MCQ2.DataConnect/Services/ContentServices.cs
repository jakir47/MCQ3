using MCQ3.DataConnect.Data;
using MCQ3.DataConnect.Entities;
using MCQ3.DataConnect.Requests;
using MCQ3.DataConnect.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace MCQ3.DataConnect.Services;

public class SubjectService(AppDbContext dbContext)
{
    public async Task<IEnumerable<SubjectViewModel>> GetAllAsync(Guid teacherId, bool isAdmin)
    {
        var query = dbContext.Subjects
            .Include(s => s.Teacher)
            .Include(s => s.Chapters)
                .ThenInclude(c => c.Questions)
            .AsQueryable();
        
        if (!isAdmin)
            query = query.Where(s => s.TeacherId == teacherId);

        var subjects = await query.ToListAsync();
        
        return subjects.Select(s => new SubjectViewModel(
            s.Id, s.Title, s.Description, s.IsArchived,
            s.TeacherId, s.Teacher?.FullName ?? "Unknown", s.CreatedAt,
            s.Chapters?.Count ?? 0,
            s.Chapters?.SelectMany(c => c.Questions ?? Enumerable.Empty<Question>()).Count() ?? 0
        )).ToList();
    }

    public async Task<SubjectViewModel?> GetByIdAsync(Guid id)
    {
        var subject = await dbContext.Subjects
            .Include(s => s.Teacher)
            .Include(s => s.Chapters)
            .FirstOrDefaultAsync(s => s.Id == id);
        if (subject == null) return null;

        return new SubjectViewModel(
            subject.Id, subject.Title, subject.Description, subject.IsArchived,
            subject.TeacherId, subject.Teacher.FullName, subject.CreatedAt,
            subject.Chapters.Count, subject.Chapters.SelectMany(c => c.Questions).Count()
        );
    }

    public async Task<SubjectViewModel> CreateAsync(Guid teacherId, CreateSubjectRequest request)
    {
        var subject = new Subject
        {
            TeacherId = teacherId,
            Title = request.Title,
            Description = request.Description
        };

        dbContext.Subjects.Add(subject);
        await dbContext.SaveChangesAsync();

        var teacher = await dbContext.Users.FindAsync(teacherId);
        return new SubjectViewModel(
            subject.Id, subject.Title, subject.Description, subject.IsArchived,
            subject.TeacherId, teacher?.FullName ?? "", subject.CreatedAt, 0, 0
        );
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateSubjectRequest request)
    {
        var subject = await dbContext.Subjects.FindAsync(id);
        if (subject == null) return false;

        if (request.Title != null) subject.Title = request.Title;
        if (request.Description != null) subject.Description = request.Description;
        if (request.IsArchived.HasValue) subject.IsArchived = request.IsArchived.Value;

        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var subject = await dbContext.Subjects.FindAsync(id);
        if (subject == null) return false;

        subject.IsArchived = true;
        await dbContext.SaveChangesAsync();
        return true;
    }
}

public class ChapterService(AppDbContext dbContext)
{
    public async Task<IEnumerable<ChapterViewModel>> GetBySubjectAsync(Guid subjectId)
    {
        return await dbContext.Chapters
            .Where(c => c.SubjectId == subjectId)
            .Select(c => new ChapterViewModel(
                c.Id, c.Title, c.Description, c.OrderIndex, c.IsArchived,
                c.SubjectId, c.CreatedAt, c.Questions.Count, c.Exams.Count, c.Enrolments.Count
            )).ToListAsync();
    }

    public async Task<ChapterViewModel?> GetByIdAsync(Guid id)
    {
        var chapter = await dbContext.Chapters.FindAsync(id);
        if (chapter == null) return null;

        return new ChapterViewModel(
            chapter.Id, chapter.Title, chapter.Description, chapter.OrderIndex, chapter.IsArchived,
            chapter.SubjectId, chapter.CreatedAt, chapter.Questions.Count, chapter.Exams.Count, chapter.Enrolments.Count
        );
    }

    public async Task<ChapterViewModel> CreateAsync(Guid subjectId, CreateChapterRequest request)
    {
        var chapter = new Chapter
        {
            SubjectId = subjectId,
            Title = request.Title,
            Description = request.Description,
            OrderIndex = request.OrderIndex
        };

        dbContext.Chapters.Add(chapter);
        await dbContext.SaveChangesAsync();

        return new ChapterViewModel(
            chapter.Id, chapter.Title, chapter.Description, chapter.OrderIndex, chapter.IsArchived,
            chapter.SubjectId, chapter.CreatedAt, 0, 0, 0
        );
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateChapterRequest request)
    {
        var chapter = await dbContext.Chapters.FindAsync(id);
        if (chapter == null) return false;

        if (request.Title != null) chapter.Title = request.Title;
        if (request.Description != null) chapter.Description = request.Description;
        if (request.OrderIndex.HasValue) chapter.OrderIndex = request.OrderIndex.Value;
        if (request.IsArchived.HasValue) chapter.IsArchived = request.IsArchived.Value;

        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var chapter = await dbContext.Chapters.FindAsync(id);
        if (chapter == null) return false;

        chapter.IsArchived = true;
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ReorderAsync(Guid subjectId, List<Guid> chapterIds)
    {
        var chapters = await dbContext.Chapters.Where(c => c.SubjectId == subjectId).ToListAsync();
        for (int i = 0; i < chapterIds.Count; i++)
        {
            var chapter = chapters.FirstOrDefault(c => c.Id == chapterIds[i]);
            if (chapter != null) chapter.OrderIndex = i;
        }
        await dbContext.SaveChangesAsync();
        return true;
    }
}