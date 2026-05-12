using MCQ3.DataConnect.Requests;
using MCQ3.DataConnect.Responses;
using MCQ3.DataConnect.Services;
using MCQ3.DataConnect.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MCQ3.API.Controllers;

[ApiController]
[Route("api/v1/users")]
public class UsersController([FromServices] UserService userService) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        var users = await userService.GetAllAsync();
        return Ok(new ApiResponse<IEnumerable<UserResponse>>(true, users));
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await userService.GetByIdAsync(id);
        if (user == null)
            return NotFound(new ApiResponse<object>(false, null, new ApiError("NOT_FOUND", "User not found")));
        return Ok(new ApiResponse<UserResponse>(true, user));
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
    {
        var user = await userService.CreateAsync(request);
        if (user == null)
            return BadRequest(new ApiResponse<object>(false, null, new ApiError("ALREADY_EXISTS", "Email already exists")));
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, new ApiResponse<UserResponse>(true, user));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserRequest request)
    {
        var result = await userService.UpdateAsync(id, request);
        if (!result)
            return NotFound(new ApiResponse<object>(false, null, new ApiError("NOT_FOUND", "User not found")));
        return Ok(new ApiResponse<bool>(true, true));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await userService.DeleteAsync(id);
        if (!result)
            return NotFound(new ApiResponse<object>(false, null, new ApiError("NOT_FOUND", "User not found")));
        return Ok(new ApiResponse<bool>(true, true));
    }

    [HttpGet("teachers")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetTeachers()
    {
        var teachers = await userService.GetTeachersAsync();
        return Ok(new ApiResponse<IEnumerable<TeacherViewModel>>(true, teachers));
    }

    [HttpPost("teachers")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateTeacher([FromBody] CreateTeacherRequest request)
    {
        var userId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
        var teacher = await userService.CreateTeacherAsync(request, userId);
        if (teacher == null)
            return BadRequest(new ApiResponse<object>(false, null, new ApiError("ALREADY_EXISTS", "Email already exists")));
        return CreatedAtAction(nameof(GetById), new { id = teacher.Id }, new ApiResponse<TeacherViewModel>(true, teacher));
    }

    [HttpPut("teachers/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateTeacher(Guid id, [FromBody] UpdateTeacherRequest request)
    {
        var result = await userService.UpdateTeacherAsync(id, request);
        if (!result)
            return NotFound(new ApiResponse<object>(false, null, new ApiError("NOT_FOUND", "Teacher not found")));
        return Ok(new ApiResponse<bool>(true, true));
    }

    [HttpGet("students")]
    [Authorize(Roles = "Teacher,Admin")]
    public async Task<IActionResult> GetStudents()
    {
        var students = await userService.GetStudentsAsync();
        return Ok(new ApiResponse<IEnumerable<StudentViewModel>>(true, students));
    }

    [HttpPost("students")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateStudent([FromBody] CreateStudentRequest request)
    {
        var userId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
        var student = await userService.CreateStudentAsync(request, userId);
        if (student == null)
            return BadRequest(new ApiResponse<object>(false, null, new ApiError("ALREADY_EXISTS", "Email already exists")));
        return CreatedAtAction(nameof(GetById), new { id = student.Id }, new ApiResponse<StudentViewModel>(true, student));
    }

    [HttpPut("students/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateStudent(Guid id, [FromBody] UpdateStudentRequest request)
    {
        var result = await userService.UpdateStudentAsync(id, request);
        if (!result)
            return NotFound(new ApiResponse<object>(false, null, new ApiError("NOT_FOUND", "Student not found")));
        return Ok(new ApiResponse<bool>(true, true));
    }

    [HttpDelete("students/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteStudent(Guid id)
    {
        var result = await userService.DeleteStudentAsync(id);
        if (!result)
            return NotFound(new ApiResponse<object>(false, null, new ApiError("NOT_FOUND", "Student not found")));
        return Ok(new ApiResponse<bool>(true, true));
    }
}