using MCQ3.DataConnect.Requests;
using MCQ3.DataConnect.Responses;
using MCQ3.DataConnect.Services;
using MCQ3.DataConnect.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ExamViewModel = MCQ3.DataConnect.ViewModels.ExamViewModel;
using AttemptSummary = MCQ3.DataConnect.Services.AttemptSummary;

namespace MCQ3.API.Controllers;

[ApiController]
[Route("api/v1/exams")]
[Authorize]
public class ExamsController([FromServices] ExamService examService) : ControllerBase
{
    [HttpGet("chapter/{chapterId}")]
    [Authorize(Roles = "Teacher,Admin")]
    public async Task<IActionResult> GetByChapter(Guid chapterId)
    {
        var exams = await examService.GetByChapterAsync(chapterId);
        return Ok(new ApiResponse<IEnumerable<ExamViewModel>>(true, exams));
    }

    [HttpGet("published")]
    public async Task<IActionResult> GetPublished()
    {
        var userId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
        var userRole = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
        
        if (userRole == "Student")
        {
            var studentExams = await examService.GetPublishedForStudentAsync(userId);
            return Ok(new ApiResponse<IEnumerable<StudentExamViewModel>>(true, studentExams));
        }
        
        var exams = await examService.GetPublishedAsync();
        return Ok(new ApiResponse<IEnumerable<ExamViewModel>>(true, exams));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var exam = await examService.GetByIdAsync(id);
        if (exam == null)
            return NotFound(new ApiResponse<object>(false, null, new ApiError("NOT_FOUND", "Exam not found")));
        return Ok(new ApiResponse<ExamViewModel>(true, exam));
    }

    [HttpPost("chapter/{chapterId}")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> Create(Guid chapterId, [FromBody] CreateExamRequest request)
    {
        var exam = await examService.CreateAsync(chapterId, request);
        return CreatedAtAction(nameof(GetById), new { id = exam.Id }, new ApiResponse<ExamViewModel>(true, exam));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateExamRequest request)
    {
        var result = await examService.UpdateAsync(id, request);
        if (!result)
            return NotFound(new ApiResponse<object>(false, null, new ApiError("NOT_FOUND", "Exam not found")));
        return Ok(new ApiResponse<bool>(true, true));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await examService.DeleteAsync(id);
        if (!result)
            return NotFound(new ApiResponse<object>(false, null, new ApiError("NOT_FOUND", "Exam not found")));
        return Ok(new ApiResponse<bool>(true, true));
    }

    [HttpPost("{id}/publish")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> Publish(Guid id)
    {
        var result = await examService.PublishAsync(id);
        if (!result)
            return BadRequest(new ApiResponse<object>(false, null, new ApiError("CANNOT_PUBLISH", "Cannot publish exam")));
        return Ok(new ApiResponse<bool>(true, true));
    }

    [HttpPost("{id}/unpublish")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> Unpublish(Guid id)
    {
        var result = await examService.UnpublishAsync(id);
        if (!result)
            return BadRequest(new ApiResponse<object>(false, null, new ApiError("CANNOT_UNPUBLISH", "Cannot unpublish exam")));
        return Ok(new ApiResponse<bool>(true, true));
    }

    [HttpPost("{id}/archive")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> Archive(Guid id)
    {
        var result = await examService.ArchiveAsync(id);
        if (!result)
            return BadRequest(new ApiResponse<object>(false, null, new ApiError("CANNOT_ARCHIVE", "Cannot archive exam")));
        return Ok(new ApiResponse<bool>(true, true));
    }

    [HttpPost("{id}/duplicate")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> Duplicate(Guid id, [FromQuery] Guid? targetChapterId = null)
    {
        var exam = await examService.DuplicateAsync(id, targetChapterId);
        if (exam == null)
            return NotFound(new ApiResponse<object>(false, null, new ApiError("NOT_FOUND", "Exam not found")));
        return Ok(new ApiResponse<ExamViewModel>(true, exam));
    }

    [HttpGet("{id}/submissions")]
    [Authorize(Roles = "Teacher,Admin")]
    public async Task<IActionResult> GetSubmissions(Guid id)
    {
        var submissions = await examService.GetSubmissionsAsync(id);
        return Ok(new ApiResponse<IEnumerable<AttemptSummary>>(true, submissions));
    }

    [HttpPost("{id}/results/release")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> ReleaseResults(Guid id)
    {
        var result = await examService.ReleaseResultsAsync(id);
        return Ok(new ApiResponse<bool>(true, result));
    }

    [HttpPost("{id}/results/release/{attemptId}")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> ReleaseSingleResult(Guid id, Guid attemptId)
    {
        var result = await examService.ReleaseResultsAsync(id, attemptId);
        return Ok(new ApiResponse<bool>(true, result));
    }

    [HttpGet("{id}/export")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> Export(Guid id, [FromQuery] string format = "csv")
    {
        var data = await examService.ExportResultsAsync(id, format);
        return File(data, "text/csv", $"results_{id}.{format}");
    }
}