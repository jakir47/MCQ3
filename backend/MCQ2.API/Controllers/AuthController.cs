using MCQ3.DataConnect.Requests;
using MCQ3.DataConnect.Responses;
using MCQ3.DataConnect.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MCQ3.API.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController([FromServices] AuthService authService, ILogger<AuthController> logger)
    : ControllerBase
{
    private readonly ILogger<AuthController> _logger = logger;

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await authService.LoginAsync(request);
        if (result == null)
            return Unauthorized(new ApiResponse<object>(false, null, new ApiError("INVALID_CREDENTIALS", "Invalid email or password")));
        return Ok(new ApiResponse<LoginResponse>(true, result));
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        var userId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
        await authService.LogoutAsync(userId, null);
        return Ok(new ApiResponse<bool>(true, true));
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshRequest request)
    {
        var result = await authService.RefreshAsync(request);
        if (result == null)
            return Unauthorized(new ApiResponse<object>(false, null, new ApiError("INVALID_TOKEN", "Invalid or expired refresh token")));
        return Ok(new ApiResponse<TokenResponse>(true, result));
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
    {
        var result = await authService.ForgotPasswordAsync(request);
        return Ok(new ApiResponse<bool>(true, result));
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        var result = await authService.ResetPasswordAsync(request);
        if (!result)
            return BadRequest(new ApiResponse<object>(false, null, new ApiError("INVALID_TOKEN", "Invalid or expired reset token")));
        return Ok(new ApiResponse<bool>(true, true));
    }

    [HttpPost("verify-email")]
    public async Task<IActionResult> VerifyEmail([FromQuery] string token)
    {
        var result = await authService.VerifyEmailAsync(token);
        if (!result)
            return BadRequest(new ApiResponse<object>(false, null, new ApiError("INVALID_TOKEN", "Invalid or expired verification token")));
        return Ok(new ApiResponse<bool>(true, true));
    }

    [HttpPost("resend-verification")]
    public async Task<IActionResult> ResendVerification([FromBody] ResendVerificationRequest request)
    {
        var result = await authService.ResendVerificationAsync(request);
        return Ok(new ApiResponse<bool>(true, result));
    }

    [HttpPost("change-password")]
    [Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        var userId = Guid.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
        var result = await authService.ChangePasswordAsync(userId, request);
        if (!result)
            return BadRequest(new ApiResponse<object>(false, null, new ApiError("INVALID_PASSWORD", "Current password is incorrect")));
        return Ok(new ApiResponse<bool>(true, true));
    }
}