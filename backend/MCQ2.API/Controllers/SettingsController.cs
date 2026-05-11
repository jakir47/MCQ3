using MCQ3.DataConnect.Responses;
using MCQ3.DataConnect.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MCQ3.API.Controllers;

[ApiController]
[Route("api/v1/settings")]
[Authorize(Roles = "Admin")]
public class SettingsController([FromServices] SettingsService settingsService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var settings = await settingsService.GetAllAsync();
        return Ok(new ApiResponse<Dictionary<string, string>>(true, settings));
    }

    [HttpGet("{key}")]
    public async Task<IActionResult> Get(string key)
    {
        var value = await settingsService.GetAsync(key);
        if (value == null) return NotFound(new ApiResponse<object>(false, "Setting not found"));
        return Ok(new ApiResponse<string>(true, value));
    }

    [HttpPut("{key}")]
    public async Task<IActionResult> Set(string key, [FromBody] string value)
    {
        var result = await settingsService.SetAsync(key, value);
        if (!result) return BadRequest(new ApiResponse<object>(false, "Failed to update setting"));
        return Ok(new ApiResponse<bool>(true, true));
    }

    [HttpPut]
    public async Task<IActionResult> SetMany([FromBody] Dictionary<string, string> settings)
    {
        await settingsService.SetManyAsync(settings);
        return Ok(new ApiResponse<bool>(true, true));
    }
}