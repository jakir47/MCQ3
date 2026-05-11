using System.Security.Claims;
using MCQ3.DataConnect.Responses;
using MCQ3.DataConnect.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MCQ3.API.Controllers;

[ApiController]
[Route("api/v1/notifications")]
[Authorize]
public class NotificationsController([FromServices] NotificationService notificationService) : ControllerBase
{
    private Guid GetUserId() => Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var notifications = await notificationService.GetByUserAsync(GetUserId());
        return Ok(new ApiResponse<IEnumerable<object>>(true, notifications));
    }

    [HttpGet("unread-count")]
    public async Task<IActionResult> GetUnreadCount()
    {
        var count = await notificationService.GetUnreadCountAsync(GetUserId());
        return Ok(new ApiResponse<int>(true, count));
    }

    [HttpPut("{id}/read")]
    public async Task<IActionResult> MarkAsRead(Guid id)
    {
        var result = await notificationService.MarkAsReadAsync(id);
        return Ok(new ApiResponse<bool>(true, result));
    }

    [HttpPut("read-all")]
    public async Task<IActionResult> MarkAllAsRead()
    {
        await notificationService.MarkAllAsReadAsync(GetUserId());
        return Ok(new ApiResponse<bool>(true, true));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await notificationService.DeleteAsync(id);
        return Ok(new ApiResponse<bool>(true, result));
    }
}