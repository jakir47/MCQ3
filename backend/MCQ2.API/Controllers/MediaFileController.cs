using System.Security.Claims;
using MCQ3.DataConnect.Enums;
using MCQ3.DataConnect.Responses;
using MCQ3.DataConnect.Services;
using MCQ3.DataConnect.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MCQ3.API.Controllers;

[ApiController]
[Route("api/media-file")]
[Authorize]
public class MediaFileController([FromServices] MediaFileService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Upload([FromForm] IFormFile file, CancellationToken ct)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var parsed))
            return Unauthorized(new ApiResponse<object>(false, null, new ApiError("UNAUTHORIZED", "Unauthorized")));

        var (success, dto, error) = await service.UploadAsync(file, parsed, ct);
        if (!success)
            return BadRequest(new ApiResponse<object>(false, null, new ApiError("BAD_REQUEST", error ?? "Upload failed")));

        return StatusCode(201, new ApiResponse<MediaFileDto>(true, dto));
    }

    [HttpPost("link")]
    public async Task<IActionResult> UploadVideoLink([FromBody] UploadVideoLinkRequest body, CancellationToken ct)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var parsed))
            return Unauthorized(new ApiResponse<object>(false, null, new ApiError("UNAUTHORIZED", "Unauthorized")));

        var (success, dto, error) = await service.UploadVideoLinkAsync(body.VideoUrl, body.Title, parsed, ct);
        if (!success)
            return BadRequest(new ApiResponse<object>(false, null, new ApiError("BAD_REQUEST", error ?? "Failed to save video link")));

        return StatusCode(201, new ApiResponse<MediaFileDto>(true, dto));
    }

    [HttpPost("bulk")]
    public async Task<IActionResult> UploadBulk([FromForm] IFormFileCollection files, CancellationToken ct)
    {
        if (files.Count > 20)
            return BadRequest(new ApiResponse<object>(false, null, new ApiError("BAD_REQUEST", "Maximum 20 files per request.")));

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var parsed))
            return Unauthorized(new ApiResponse<object>(false, null, new ApiError("UNAUTHORIZED", "Unauthorized")));

        var result = await service.UploadBulkAsync(files.ToList(), parsed, ct);
        return Ok(new ApiResponse<BulkUploadResultDto>(true, result));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var dto = await service.GetByIdAsync(id, ct);
        if (dto == null)
            return NotFound(new ApiResponse<object>(false, null, new ApiError("NOT_FOUND", "File not found.")));

        return Ok(new ApiResponse<MediaFileDto>(true, dto));
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged(
        [FromQuery] string? type = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken ct = default)
    {
        MediaType? mediaTypeFilter = null;
        if (!string.IsNullOrEmpty(type))
        {
            if (Enum.TryParse<MediaType>(type, true, out var m))
                mediaTypeFilter = m;
            else
                return BadRequest(new ApiResponse<object>(false, null, new ApiError("BAD_REQUEST", "Invalid type. Use Image, Audio, or Document.")));
        }

        var result = await service.GetPagedAsync(
            mediaTypeFilter,
            Math.Max(page, 1),
            Math.Min(pageSize, 100),
            ct);

        return Ok(new ApiResponse<PagedResult<MediaFileDto>>(true, result));
    }

    [HttpPatch("{id:guid}/alt")]
    public async Task<IActionResult> UpdateAltText(Guid id, [FromBody] UpdateAltTextRequest body, CancellationToken ct)
    {
        var dto = await service.UpdateAltTextAsync(id, body.AltText, ct);
        if (dto == null)
            return NotFound(new ApiResponse<object>(false, null, new ApiError("NOT_FOUND", "File not found.")));

        return Ok(new ApiResponse<MediaFileDto>(true, dto));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> SoftDelete(Guid id, CancellationToken ct)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out var parsed))
            return Unauthorized(new ApiResponse<object>(false, null, new ApiError("UNAUTHORIZED", "Unauthorized")));

        var (success, error) = await service.SoftDeleteAsync(id, parsed, ct);
        if (!success)
            return BadRequest(new ApiResponse<object>(false, null, new ApiError("BAD_REQUEST", error ?? "Delete failed")));

        return NoContent();
    }

    [HttpPost("{id:guid}/restore")]
    public async Task<IActionResult> Restore(Guid id, CancellationToken ct)
    {
        var dto = await service.RestoreAsync(id, ct);
        if (dto == null)
            return BadRequest(new ApiResponse<object>(false, null, new ApiError("BAD_REQUEST", "File not found or not deleted.")));

        return Ok(new ApiResponse<MediaFileDto>(true, dto));
    }

    [HttpDelete("{id:guid}/purge")]
    [Authorize(Policy = "AdminOnly")]
    public async Task<IActionResult> HardDelete(Guid id, CancellationToken ct)
    {
        var (success, error) = await service.HardDeleteAsync(id, ct);
        if (!success)
            return BadRequest(new ApiResponse<object>(false, null, new ApiError("BAD_REQUEST", error ?? "Purge failed")));

        return NoContent();
    }
}

public record UpdateAltTextRequest(string? AltText);
public record UploadVideoLinkRequest(string VideoUrl, string? Title);