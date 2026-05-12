using MCQ3.DataConnect.Requests;
using MCQ3.DataConnect.Responses;
using MCQ3.DataConnect.Services;
using MCQ3.DataConnect.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MCQ3.API.Controllers;

[ApiController]
[Route("api/v1/subjects")]
[Authorize]
public class SubjectsController([FromServices] SubjectService subjectService) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = "Teacher,Admin")]
    public async Task<IActionResult> GetAll()
    {
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
        var roleClaim = User.FindFirst(System.Security.Claims.ClaimTypes.Role);

        if (userIdClaim == null || roleClaim == null)
            return Unauthorized(new ApiResponse<object>(false, null, new ApiError("INVALID_TOKEN", "Invalid authentication token")));

        var foreignClaim = User.FindFirst("ForeignId");
        var userId = Guid.Parse(foreignClaim?.Value ?? Guid.Empty.ToString());
        var isAdmin = roleClaim.Value == "Admin";
        var subjects = await subjectService.GetAllAsync(userId, isAdmin);
        return Ok(new ApiResponse<IEnumerable<SubjectViewModel>>(true, subjects));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var subject = await subjectService.GetByIdAsync(id);
        if (subject == null)
            return NotFound(new ApiResponse<object>(false, null, new ApiError("NOT_FOUND", "Subject not found")));
        return Ok(new ApiResponse<SubjectViewModel>(true, subject));
    }

    [HttpPost]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> Create([FromBody] CreateSubjectRequest request)
    {
        var foreignClaim = User.FindFirst("ForeignId");
        var teacherId = Guid.Parse(foreignClaim?.Value ?? Guid.Empty.ToString());
        var subject = await subjectService.CreateAsync(teacherId, request);
        return CreatedAtAction(nameof(GetById), new { id = subject.Id }, new ApiResponse<SubjectViewModel>(true, subject));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Teacher,Admin")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSubjectRequest request)
    {
        var result = await subjectService.UpdateAsync(id, request);
        if (!result)
            return NotFound(new ApiResponse<object>(false, null, new ApiError("NOT_FOUND", "Subject not found")));
        return Ok(new ApiResponse<bool>(true, true));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Teacher,Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await subjectService.DeleteAsync(id);
        if (!result)
            return NotFound(new ApiResponse<object>(false, null, new ApiError("NOT_FOUND", "Subject not found")));
        return Ok(new ApiResponse<bool>(true, true));
    }

    [HttpGet("{id}/chapters")]
    public async Task<IActionResult> GetChapters(Guid id)
    {
        var subject = await subjectService.GetByIdAsync(id);
        return Ok(new ApiResponse<object>(true, subject));
    }

    [HttpPost("{id}/chapters")]
    [Authorize(Roles = "Teacher")]
    public async Task<IActionResult> CreateChapter(Guid id, [FromBody] CreateChapterRequest request)
    {
        var chapter = await subjectService.GetByIdAsync(id);
        return Ok(new ApiResponse<object>(true, chapter));
    }
}