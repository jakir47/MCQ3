using System.Security.Claims;
using MCQ3.DataConnect.Responses;
using MCQ3.DataConnect.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MCQ3.API.Controllers;

[ApiController]
[Route("api/v1/audit-logs")]
[Authorize(Roles = "Admin,Teacher")]
public class AuditLogsController([FromServices] AuditLogService auditLogService) : ControllerBase
{
    private Guid GetUserId() => Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

    [HttpGet]
    public async Task<IActionResult> GetLogs([FromQuery] string? targetType = null, [FromQuery] Guid? targetId = null, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        IEnumerable<object> logs;
        if (targetType != null && targetId.HasValue)
        {
            logs = await auditLogService.GetByTargetAsync(targetType, targetId.Value);
        }
        else
        {
            logs = await auditLogService.GetByActorAsync(GetUserId(), page, pageSize);
        }
        return Ok(new ApiResponse<IEnumerable<object>>(true, logs));
    }

    [HttpGet("export")]
    public async Task<IActionResult> Export([FromQuery] DateTime? from, [FromQuery] DateTime? to)
    {
        var data = await auditLogService.ExportAsync(from, to);
        return File(data, "text/csv", $"audit-logs-{DateTime.Now:yyyyMMdd}.csv");
    }
}