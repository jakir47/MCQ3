using MCQ3.DataConnect.Requests;
using MCQ3.DataConnect.Responses;
using MCQ3.DataConnect.Services;
using MCQ3.DataConnect.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MCQ3.API.Controllers;

[ApiController]
[Route("api/v1/student-assignments")]
[Authorize(Roles = "Teacher")]
public class StudentAssignmentsController([FromServices] UserService userService) : ControllerBase
{
    [HttpPost("assign")]
    public async Task<IActionResult> AssignStudent([FromBody] AssignStudentRequest request)
    {
        var foreignClaim = User.FindFirst("ForeignId");
        var teacherId = Guid.Parse(foreignClaim?.Value ?? Guid.Empty.ToString());
        var result = await userService.AssignStudentToChapterAsync(request.StudentId, request.ChapterId, teacherId);
        if (!result)
            return BadRequest(new ApiResponse<object>(false, null, new ApiError("INVALID", "Failed to assign student")));
        return Ok(new ApiResponse<bool>(true, true));
    }

    [HttpDelete("{studentId}/chapter/{chapterId}")]
    public async Task<IActionResult> RemoveStudent(Guid studentId, Guid chapterId)
    {
        var foreignClaim = User.FindFirst("ForeignId");
        var teacherId = Guid.Parse(foreignClaim?.Value ?? Guid.Empty.ToString());
        var result = await userService.RemoveStudentFromChapterAsync(studentId, chapterId, teacherId);
        if (!result)
            return NotFound(new ApiResponse<object>(false, null, new ApiError("NOT_FOUND", "Assignment not found or unauthorized")));
        return Ok(new ApiResponse<bool>(true, true));
    }

    [HttpGet("chapter/{chapterId}")]
    public async Task<IActionResult> GetChapterAssignments(Guid chapterId)
    {
        var foreignClaim = User.FindFirst("ForeignId");
        var teacherId = Guid.Parse(foreignClaim?.Value ?? Guid.Empty.ToString());
        var assignments = await userService.GetStudentAssignmentsAsync(chapterId, teacherId);
        return Ok(new ApiResponse<IEnumerable<StudentChapterViewModel>>(true, assignments));
    }
}