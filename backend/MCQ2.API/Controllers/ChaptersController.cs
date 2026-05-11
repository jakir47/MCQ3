using MCQ3.DataConnect.Requests;
using MCQ3.DataConnect.Responses;
using MCQ3.DataConnect.Services;
using MCQ3.DataConnect.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MCQ3.API.Controllers;

[ApiController]
[Route("api/v1/chapters")]
[Authorize]
public class ChaptersController([FromServices] ChapterService chapterService) : ControllerBase
{
    [HttpGet("subject/{subjectId}")]
    public async Task<IActionResult> GetBySubject(Guid subjectId)
    {
        var chapters = await chapterService.GetBySubjectAsync(subjectId);
        return Ok(new ApiResponse<IEnumerable<ChapterViewModel>>(true, chapters));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var chapter = await chapterService.GetByIdAsync(id);
        if (chapter == null)
            return NotFound(new ApiResponse<object>(false, null, new ApiError("NOT_FOUND", "Chapter not found")));
        return Ok(new ApiResponse<ChapterViewModel>(true, chapter));
    }

    [HttpPost("subject/{subjectId}")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> Create(Guid subjectId, [FromBody] CreateChapterRequest request)
    {
        var chapter = await chapterService.CreateAsync(subjectId, request);
        return CreatedAtAction(nameof(GetById), new { id = chapter.Id }, new ApiResponse<ChapterViewModel>(true, chapter));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateChapterRequest request)
    {
        var result = await chapterService.UpdateAsync(id, request);
        if (!result)
            return NotFound(new ApiResponse<object>(false, null, new ApiError("NOT_FOUND", "Chapter not found")));
        return Ok(new ApiResponse<bool>(true, true));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await chapterService.DeleteAsync(id);
        if (!result)
            return NotFound(new ApiResponse<object>(false, null, new ApiError("NOT_FOUND", "Chapter not found")));
        return Ok(new ApiResponse<bool>(true, true));
    }
}