using MCQ3.DataConnect.Data;
using MCQ3.DataConnect.Entities;
using MCQ3.DataConnect.Enums;
using MCQ3.DataConnect.Requests;
using MCQ3.DataConnect.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace MCQ3.DataConnect.Services;

public class QuestionService(AppDbContext dbContext)
{
    public async Task<IEnumerable<QuestionViewModel>> GetByChapterAsync(Guid chapterId)
    {
        var questions = await dbContext.Questions
            .Where(q => q.ChapterId == chapterId)
            .Include(q => q.AnswerOptions.OrderBy(o => o.OrderIndex))
            .OrderBy(q => q.CreatedAt)
            .ToListAsync();

        return questions.Select(MapToViewModel);
    }

    public async Task<QuestionViewModel?> GetByIdAsync(Guid id)
    {
        var question = await dbContext.Questions
            .Include(q => q.AnswerOptions.OrderBy(o => o.OrderIndex))
            .FirstOrDefaultAsync(q => q.Id == id);

        return question == null ? null : MapToViewModel(question);
    }

    public async Task<QuestionViewModel> CreateAsync(Guid chapterId, CreateQuestionRequest request)
    {
        var question = new Question
        {
            ChapterId = chapterId,
            StemText = request.StemText,
            StemImagePath = request.StemImagePath,
            StemAudioPath = request.StemAudioPath,
            StemVideoUrl = request.StemVideoUrl,
            Explanation = request.Explanation,
            PositiveMarks = request.PositiveMarks,
            NegativeMarks = request.NegativeMarks,
            Difficulty = Enum.TryParse<Difficulty>(request.Difficulty, out var diff) ? diff : Difficulty.Medium,
            Tags = request.Tags
        };

        dbContext.Questions.Add(question);

        int orderIndex = 0;
        foreach (var option in request.Options)
        {
            question.AnswerOptions.Add(new AnswerOption
            {
                OptionText = option.OptionText,
                ImagePath = option.ImagePath,
                AudioPath = option.AudioPath,
                IsCorrect = option.IsCorrect,
                OrderIndex = orderIndex++
            });
        }

        await dbContext.SaveChangesAsync();

        return (await GetByIdAsync(question.Id))!;
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateQuestionRequest request)
    {
        var question = await dbContext.Questions
            .Include(q => q.AnswerOptions)
            .FirstOrDefaultAsync(q => q.Id == id);

        if (question == null) return false;

        if (request.StemText != null) question.StemText = request.StemText;
        if (request.StemImagePath != null) question.StemImagePath = request.StemImagePath;
        if (request.StemAudioPath != null) question.StemAudioPath = request.StemAudioPath;
        if (request.StemVideoUrl != null) question.StemVideoUrl = request.StemVideoUrl;
        if (request.Explanation != null) question.Explanation = request.Explanation;
        if (request.PositiveMarks.HasValue) question.PositiveMarks = request.PositiveMarks.Value;
        if (request.NegativeMarks.HasValue) question.NegativeMarks = request.NegativeMarks.Value;
        if (request.Difficulty != null && Enum.TryParse<Difficulty>(request.Difficulty, out var diff)) question.Difficulty = diff;
        if (request.Tags != null) question.Tags = request.Tags;

        if (request.Options != null)
        {
            var existingOptions = question.AnswerOptions.ToList();
            foreach (var existing in existingOptions)
            {
                dbContext.AnswerOptions.Remove(existing);
            }

            int orderIndex = 0;
            foreach (var option in request.Options)
            {
                question.AnswerOptions.Add(new AnswerOption
                {
                    Id = option.Id,
                    OptionText = option.OptionText,
                    ImagePath = option.ImagePath,
                    AudioPath = option.AudioPath,
                    IsCorrect = option.IsCorrect ?? false,
                    OrderIndex = orderIndex++
                });
            }
        }

        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var question = await dbContext.Questions.FindAsync(id);
        if (question == null) return false;

        dbContext.Questions.Remove(question);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<int> ImportFromBankAsync(Guid chapterId, List<Guid> questionIds)
    {
        var sourceQuestions = await dbContext.Questions
            .Where(q => questionIds.Contains(q.Id))
            .ToListAsync();

        foreach (var source in sourceQuestions)
        {
            var newQuestion = new Question
            {
                ChapterId = chapterId,
                SourceQuestionId = source.Id,
                StemText = source.StemText,
                StemImagePath = source.StemImagePath,
                StemAudioPath = source.StemAudioPath,
                StemVideoUrl = source.StemVideoUrl,
                Explanation = source.Explanation,
                PositiveMarks = source.PositiveMarks,
                NegativeMarks = source.NegativeMarks,
                Difficulty = source.Difficulty,
                Tags = source.Tags
            };

            dbContext.Questions.Add(newQuestion);

            var sourceOptions = await dbContext.AnswerOptions
                .Where(o => o.QuestionId == source.Id)
                .OrderBy(o => o.OrderIndex)
                .ToListAsync();

            foreach (var option in sourceOptions)
            {
                newQuestion.AnswerOptions.Add(new AnswerOption
                {
                    OptionText = option.OptionText,
                    ImagePath = option.ImagePath,
                    AudioPath = option.AudioPath,
                    IsCorrect = option.IsCorrect,
                    OrderIndex = option.OrderIndex
                });
            }
        }

        await dbContext.SaveChangesAsync();
        return sourceQuestions.Count;
    }

    public async Task<IEnumerable<QuestionViewModel>> SearchGlobalBankAsync(BankSearchParams p)
    {
        var query = dbContext.Questions
            .Where(q => q.SourceQuestionId == null)
            .Include(q => q.AnswerOptions.OrderBy(o => o.OrderIndex))
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(p.q))
        {
            var searchTerm = p.q.ToLower();
            query = query.Where(q => (q.StemText != null && q.StemText.ToLower().Contains(searchTerm)) ||
                                      (q.Tags != null && q.Tags.ToLower().Contains(searchTerm)));
        }

        if (p.difficulty.HasValue)
        {
            query = query.Where(q => q.Difficulty == p.difficulty.Value);
        }

        var skip = (p.page - 1) * p.pageSize;
        var questions = await query
            .OrderByDescending(q => q.CreatedAt)
            .Skip(skip)
            .Take(p.pageSize)
            .ToListAsync();

        return questions.Select(MapToViewModel);
    }

    private static QuestionViewModel MapToViewModel(Question q)
    {
        return new QuestionViewModel(
            q.Id,
            q.StemText,
            q.StemImagePath,
            q.StemAudioPath,
            q.StemVideoUrl,
            q.Explanation,
            q.PositiveMarks,
            q.NegativeMarks,
            q.Difficulty,
            q.Tags,
            q.ChapterId,
            q.CreatedAt,
            q.AnswerOptions.Select(o => new AnswerOptionViewModel(
                o.Id,
                o.OptionText,
                o.ImagePath,
                o.AudioPath,
                o.IsCorrect,
                o.OrderIndex
            )).ToList()
        );
    }
}