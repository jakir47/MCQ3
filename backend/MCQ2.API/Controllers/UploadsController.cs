using MCQ3.DataConnect.Responses;
using MCQ3.DataConnect.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MCQ3.API.Controllers;

[ApiController]
[Route("api/v1/uploads")]
[Authorize(Roles = "Teacher")]
public class UploadsController([FromServices] UploadService uploadService) : ControllerBase
{
    [HttpPost("image")]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest(new ApiResponse<object>(false, null, new ApiError("INVALID_FILE", "No file provided")));

        try
        {
            var teacherId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
            var url = await uploadService.UploadImageAsync(file, teacherId);
            return Ok(new ApiResponse<UploadResponse>(true, new UploadResponse(url)));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new ApiResponse<object>(false, null, new ApiError("INVALID_FILE", ex.Message)));
        }
    }

    [HttpPost("audio")]
    public async Task<IActionResult> UploadAudio(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest(new ApiResponse<object>(false, null, new ApiError("INVALID_FILE", "No file provided")));

        try
        {
            var teacherId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
            var url = await uploadService.UploadAudioAsync(file, teacherId);
            return Ok(new ApiResponse<UploadResponse>(true, new UploadResponse(url)));
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new ApiResponse<object>(false, null, new ApiError("INVALID_FILE", ex.Message)));
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] string path)
    {
        var result = await uploadService.DeleteFileAsync(path);
        return Ok(new ApiResponse<bool>(true, result));
    }
}

public record UploadResponse(string Url);