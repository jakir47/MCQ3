using MCQ3.DataConnect.Requests;
using MCQ3.DataConnect.Responses;
using MCQ3.DataConnect.Services;
using MCQ3.DataConnect.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MCQ3.API.Controllers;

[ApiController]
[Route("api/v1/questions")]
[Authorize(Roles = "Teacher,Admin")]
public class QuestionsController([FromServices] QuestionService questionService) : ControllerBase
{
    [HttpGet("chapter/{chapterId}")]
    public async Task<IActionResult> GetByChapter(Guid chapterId)
    {
        var questions = await questionService.GetByChapterAsync(chapterId);
        return Ok(new ApiResponse<IEnumerable<QuestionViewModel>>(true, questions));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var question = await questionService.GetByIdAsync(id);
        if (question == null)
            return NotFound(new ApiResponse<object>(false, null, new ApiError("NOT_FOUND", "Question not found")));
        return Ok(new ApiResponse<QuestionViewModel>(true, question));
    }

    [HttpPost("chapter/{chapterId}")]
    public async Task<IActionResult> Create(Guid chapterId, [FromBody] CreateQuestionRequest request)
    {
        var question = await questionService.CreateAsync(chapterId, request);
        return CreatedAtAction(nameof(GetById), new { id = question.Id }, new ApiResponse<QuestionViewModel>(true, question));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateQuestionRequest request)
    {
        var result = await questionService.UpdateAsync(id, request);
        if (!result)
            return NotFound(new ApiResponse<object>(false, null, new ApiError("NOT_FOUND", "Question not found")));
        return Ok(new ApiResponse<bool>(true, true));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await questionService.DeleteAsync(id);
        if (!result)
            return NotFound(new ApiResponse<object>(false, null, new ApiError("NOT_FOUND", "Question not found")));
        return Ok(new ApiResponse<bool>(true, true));
    }

    [HttpPost("chapter/{chapterId}/import")]
    public async Task<IActionResult> Import([FromBody] ImportQuestionsRequest request, Guid chapterId)
    {
        var count = await questionService.ImportFromBankAsync(chapterId, request.QuestionIds);
        return Ok(new ApiResponse<int>(true, count));
    }
}

[ApiController]
[Route("api/v1/bank")]
[Authorize(Roles = "Teacher")]
public class GlobalBankController([FromServices] QuestionService questionService) : ControllerBase
{
    [HttpGet("global")]
    public async Task<IActionResult> Search([FromQuery] BankSearchParams p)
    {
        var questions = await questionService.SearchGlobalBankAsync(p);
        return Ok(new ApiResponse<IEnumerable<QuestionViewModel>>(true, questions));
    }
}

public class ImportQuestionsRequest
{
    public List<Guid> QuestionIds { get; set; } = new();
}