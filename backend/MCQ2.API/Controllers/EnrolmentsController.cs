using MCQ3.DataConnect.Requests;
using MCQ3.DataConnect.Responses;
using MCQ3.DataConnect.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EnrolmentViewModel = MCQ3.DataConnect.ViewModels.EnrolmentViewModel;
using BulkEnrolResult = MCQ3.DataConnect.ViewModels.BulkEnrolResult;
using UpdateEnrolmentRequest = MCQ3.DataConnect.Requests.UpdateEnrolmentRequest;

namespace MCQ3.API.Controllers;

[ApiController]
[Route("api/v1/enrolments")]
[Authorize]
public class EnrolmentsController([FromServices] EnrolmentService enrolmentService) : ControllerBase
{
    [HttpGet("chapter/{chapterId}")]
    [Authorize(Roles = "Teacher,Admin")]
    public async Task<IActionResult> GetStudents(Guid chapterId)
    {
        var enrolments = await enrolmentService.GetByChapterAsync(chapterId);
        return Ok(new ApiResponse<IEnumerable<EnrolmentViewModel>>(true, enrolments));
    }

    [HttpPost("chapter/{chapterId}/enrol")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> Enrol(Guid chapterId, [FromBody] EnrolRequest request)
    {
        var teacherId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
        var result = await enrolmentService.EnrolAsync(chapterId, request.StudentId, teacherId, request.ExpiresAt);
        if (!result)
            return BadRequest(new ApiResponse<object>(false, null, new ApiError("ENROLMENT_FAILED", "Student already enrolled or not found")));
        return Ok(new ApiResponse<bool>(true, true));
    }

    [HttpPost("chapter/{chapterId}/register")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> Register(Guid chapterId, [FromBody] RegisterStudentRequest request)
    {
        var teacherId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
        var result = await enrolmentService.RegisterAndEnrolAsync(chapterId, request, teacherId);
        if (result == null)
            return BadRequest(new ApiResponse<object>(false, null, new ApiError("REGISTRATION_FAILED", "Student already enrolled or invalid data")));
        return Ok(new ApiResponse<EnrolmentViewModel>(true, result));
    }

    [HttpPost("chapter/{chapterId}/bulk")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> BulkEnrol(Guid chapterId, IFormFile file)
    {
        var teacherId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
        using var stream = file.OpenReadStream();
        var result = await enrolmentService.BulkEnrolAsync(chapterId, teacherId, stream);
        return Ok(new ApiResponse<BulkEnrolResult>(true, result));
    }

    [HttpDelete("chapter/{chapterId}/students/{studentId}")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> Remove(Guid chapterId, Guid studentId)
    {
        var result = await enrolmentService.RemoveAsync(chapterId, studentId);
        if (!result)
            return NotFound(new ApiResponse<object>(false, null, new ApiError("NOT_FOUND", "Enrolment not found")));
        return Ok(new ApiResponse<bool>(true, true));
    }

    [HttpPost("chapter/{chapterId}/students/{studentId}/reenrol")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> ReEnrol(Guid chapterId, Guid studentId)
    {
        var teacherId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
        var result = await enrolmentService.ReEnrolAsync(chapterId, studentId, teacherId);
        if (!result)
            return BadRequest(new ApiResponse<object>(false, null, new ApiError("REENROL_FAILED", "Cannot re-enrol student")));
        return Ok(new ApiResponse<bool>(true, true));
    }

    [HttpPatch("chapter/{chapterId}/students/{studentId}")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> Update(Guid chapterId, Guid studentId, [FromBody] UpdateEnrolmentRequest request)
    {
        var result = await enrolmentService.UpdateAsync(chapterId, studentId, request);
        if (!result)
            return NotFound(new ApiResponse<object>(false, null, new ApiError("NOT_FOUND", "Enrolment not found")));
        return Ok(new ApiResponse<bool>(true, true));
    }
}

public class EnrolRequest
{
    public Guid StudentId { get; set; }
    public DateTime? ExpiresAt { get; set; }
}