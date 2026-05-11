using MCQ3.DataConnect.Requests;
using MCQ3.DataConnect.Responses;
using MCQ3.DataConnect.Services;
using MCQ3.DataConnect.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AttemptResultResponse = MCQ3.DataConnect.Services.AttemptResultResponse;

namespace MCQ3.API.Controllers;

[ApiController]
[Route("api/v1/attempts")]
[Authorize(Roles = "Student")]
public class AttemptsController([FromServices] AttemptService attemptService) : ControllerBase
{
    [HttpGet("my-chapters")]
    public async Task<IActionResult> GetMyChapters()
    {
        var userId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
        var chapters = await attemptService.GetMyChaptersAsync(userId);
        return Ok(new ApiResponse<IEnumerable<ChapterWithExamsViewModel>>(true, chapters));
    }

    [HttpGet("my-attempts")]
    public async Task<IActionResult> GetMyAttempts()
    {
        var userId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
        var attempts = await attemptService.GetMyAttemptsAsync(userId);
        return Ok(new ApiResponse<IEnumerable<object>>(true, attempts));
    }

    [HttpPost("exams/{examId}/start")]
    public async Task<IActionResult> StartExam(Guid examId)
    {
        var userId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
        var result = await attemptService.StartExamAsync(examId, userId);
        if (!result.Success)
            return BadRequest(new ApiResponse<object>(false, null, new ApiError(result.ErrorCode ?? "CANNOT_START", result.ErrorMessage ?? "Cannot start exam")));
        return Ok(new ApiResponse<AttemptViewModel>(true, result.Attempt));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAttempt(Guid id)
    {
        var attempt = await attemptService.GetAttemptAsync(id);
        if (attempt == null)
            return NotFound(new ApiResponse<object>(false, null, new ApiError("NOT_FOUND", "Attempt not found")));
        return Ok(new ApiResponse<AttemptViewModel>(true, attempt));
    }

    [HttpPatch("{id}/save")]
    public async Task<IActionResult> Save(Guid id, [FromBody] SaveAttemptRequest request)
    {
        var result = await attemptService.SaveAsync(id, request);
        if (!result)
            return BadRequest(new ApiResponse<object>(false, null, new ApiError("SAVE_FAILED", "Cannot save attempt")));
        return Ok(new ApiResponse<bool>(true, true));
    }

    [HttpPost("{id}/submit")]
    public async Task<IActionResult> Submit(Guid id, [FromBody] SubmitAttemptRequest request)
    {
        var result = await attemptService.SubmitAsync(id, request);
        if (result == null)
            return BadRequest(new ApiResponse<object>(false, null, new ApiError("SUBMIT_FAILED", "Cannot submit attempt")));
        return Ok(new ApiResponse<AttemptResultResponse>(true, result));
    }

    [HttpGet("{id}/result")]
    public async Task<IActionResult> GetResult(Guid id)
    {
        var result = await attemptService.GetResultAsync(id);
        if (result == null)
            return NotFound(new ApiResponse<object>(false, null, new ApiError("NOT_FOUND", "Result not found or not released")));
        return Ok(new ApiResponse<AttemptResultResponse>(true, result));
    }

    [HttpGet("{id}/review")]
    public async Task<IActionResult> GetReview(Guid id)
    {
        var userId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
        var result = await attemptService.GetReviewAsync(id, userId);
        if (result == null)
            return NotFound(new ApiResponse<object>(false, null, new ApiError("NOT_FOUND", "Review not found or not released")));
        return Ok(new ApiResponse<ExamReviewViewModel>(true, result));
    }

    [HttpPatch("{id}/release")]
    [Authorize(Roles = "Teacher,Admin")]
    public async Task<IActionResult> ReleaseResult(Guid id)
    {
        var result = await attemptService.ReleaseResultAsync(id);
        if (!result)
            return NotFound(new ApiResponse<object>(false, null, new ApiError("NOT_FOUND", "Attempt not found")));
        return Ok(new ApiResponse<bool>(true, true));
    }
}