using MCQ3.DataConnect.Responses;
using MCQ3.DataConnect.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EnrolmentViewModel = MCQ3.DataConnect.ViewModels.EnrolmentViewModel;

namespace MCQ3.API.Controllers;

[ApiController]
[Route("api/v1/enrolments")]
[Authorize]
public class EnrolmentsController([FromServices] EnrolmentService enrolmentService) : ControllerBase
{
    [HttpGet("exam/{examId}/available-students")]
    [Authorize(Roles = "Teacher,Admin")]
    public async Task<IActionResult> GetAvailableStudentsForExam(Guid examId)
    {
        var students = await enrolmentService.GetStudentsForExamEnrolmentAsync(examId);
        return Ok(new ApiResponse<IEnumerable<object>>(true, students));
    }

    [HttpGet("exam/{examId}/enrolled")]
    [Authorize(Roles = "Teacher,Admin")]
    public async Task<IActionResult> GetEnrolledStudentsForExam(Guid examId)
    {
        var enrolments = await enrolmentService.GetByExamAsync(examId);
        return Ok(new ApiResponse<IEnumerable<EnrolmentViewModel>>(true, enrolments));
    }

    [HttpPost("exam/{examId}/enrol")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> EnrolStudentsInExam(Guid examId, [FromBody] EnrolStudentsRequest request)
    {
        var teacherId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
        var count = await enrolmentService.EnrolStudentsInExamAsync(examId, request.StudentIds, teacherId, request.ExpiresAt);
        return Ok(new ApiResponse<int>(true, count));
    }

    [HttpPost("exam/{examId}/unenrol")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> UnenrollStudentsFromExam(Guid examId, [FromBody] UnenrollStudentsRequest request)
    {
        var count = await enrolmentService.UnenrollStudentsFromExamAsync(examId, request.StudentIds);
        return Ok(new ApiResponse<int>(true, count));
    }
}

public class EnrolStudentsRequest
{
    public List<Guid> StudentIds { get; set; } = new();
    public DateTime? ExpiresAt { get; set; }
}

public class UnenrollStudentsRequest
{
    public List<Guid> StudentIds { get; set; } = new();
}