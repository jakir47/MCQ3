using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MCQ3.DataConnect.Data;
using MCQ3.DataConnect.Entities;
using MCQ3.DataConnect.Requests;
using MCQ3.DataConnect.Responses;
using MCQ3.DataConnect.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace MCQ3.DataConnect.Services;

public class AuthService(
    AppDbContext dbContext,
    IConfiguration configuration,
    ILogger<AuthService> logger,
    IEmailService emailService)
{
    private readonly IEmailService _emailService = emailService;

    public async Task<LoginResponse?> LoginAsync(LoginRequest request)
    {
        var user = await dbContext.Users
            .Include(u => u.RoleEntity)
            .Include(u => u.TeacherProfile)
            .Include(u => u.StudentProfile)
            .FirstOrDefaultAsync(u => 
                (u.Email == request.Email || u.Username == request.Email) && u.IsActive);
        if (user == null || !PasswordHelper.VerifyPassword(request.Password, user.PasswordHash))
        {
            return null;
        }

        var tokens = await GenerateTokensAsync(user);

        return new LoginResponse(
            user.Id,
            user.Username,
            user.Email,
            user.FullName,
            user.RoleEntity?.Name ?? "Unknown",
            tokens.AccessToken,
            tokens.RefreshToken,
            user.TempPassword
        );
    }

    public async Task<bool> LogoutAsync(Guid userId, string? refreshToken)
    {
        if (!string.IsNullOrEmpty(refreshToken))
        {
            var token = await dbContext.RefreshTokens
                .FirstOrDefaultAsync(t => t.Token == refreshToken && t.UserId == userId);
            if (token != null)
            {
                token.IsRevoked = true;
                await dbContext.SaveChangesAsync();
            }
        }
        return true;
    }

    public async Task<TokenResponse?> RefreshAsync(RefreshRequest request)
    {
        var token = await dbContext.RefreshTokens
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.Token == request.RefreshToken && !t.IsRevoked && t.ExpiresAt > DateTime.UtcNow);

        if (token == null) return null;

        token.IsRevoked = true;
        await dbContext.SaveChangesAsync();

        return await GenerateTokensAsync(token.User);
    }

    public async Task<bool> ForgotPasswordAsync(ForgotPasswordRequest request)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (user == null) return false;

        var reset = new PasswordReset
        {
            UserId = user.Id,
            Token = Guid.NewGuid().ToString("N"),
            ExpiresAt = DateTime.UtcNow.AddHours(24)
        };

        dbContext.PasswordResets.Add(reset);
        await dbContext.SaveChangesAsync();

        // TODO: Send email with reset link
        logger.LogInformation("Password reset token for {Email}: {Token}", user.Email, reset.Token);
        return true;
    }

    public async Task<bool> ResetPasswordAsync(ResetPasswordRequest request)
    {
        var reset = await dbContext.PasswordResets
            .FirstOrDefaultAsync(r => r.Token == request.Token && r.UsedAt == null && r.ExpiresAt > DateTime.UtcNow);

        if (reset == null) return false;

        var user = await dbContext.Users.FindAsync(reset.UserId);
        if (user == null) return false;

        user.PasswordHash = PasswordHelper.HashPassword(request.NewPassword);
        user.TempPassword = false;
        reset.UsedAt = DateTime.UtcNow;

        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> VerifyEmailAsync(string token)
    {
        var verification = await dbContext.EmailVerifications
            .FirstOrDefaultAsync(v => v.Token == token && v.UsedAt == null && v.ExpiresAt > DateTime.UtcNow);

        if (verification == null) return false;

        var user = await dbContext.Users.FindAsync(verification.UserId);
        if (user == null) return false;

        user.IsActive = true;
        verification.UsedAt = DateTime.UtcNow;

        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ResendVerificationAsync(ResendVerificationRequest request)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (user == null || user.IsActive) return false;

        var existing = await dbContext.EmailVerifications
            .Where(v => v.UserId == user.Id && v.UsedAt == null)
            .ToListAsync();
        dbContext.EmailVerifications.RemoveRange(existing);

        var verification = new EmailVerification
        {
            UserId = user.Id,
            Token = Guid.NewGuid().ToString("N"),
            ExpiresAt = DateTime.UtcNow.AddHours(24)
        };

        dbContext.EmailVerifications.Add(verification);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Email verification token for {Email}: {Token}", user.Email, verification.Token);
        return true;
    }

    public async Task<bool> ChangePasswordAsync(Guid userId, ChangePasswordRequest request)
    {
        var user = await dbContext.Users.FindAsync(userId);
        if (user == null) return false;

        if (!PasswordHelper.VerifyPassword(request.CurrentPassword, user.PasswordHash))
            return false;

        user.PasswordHash = PasswordHelper.HashPassword(request.NewPassword);
        user.TempPassword = false;

        await dbContext.SaveChangesAsync();
        return true;
    }

    public string GenerateAccessToken(UserAccount user)
    {
        var foreignId = user.RoleEntity?.Name == "Student"
            ? user.StudentProfile?.UserId
            : user.Id;
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim("Username", user.Username),
            new Claim("ForeignId", foreignId.ToString()),
            new Claim(ClaimTypes.Name, user.FullName),
            new Claim(ClaimTypes.Role, user.RoleEntity?.Name ?? "Unknown"),
            new Claim(ClaimTypes.Email, user.Email),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(configuration["Jwt:AccessTokenExpiryMinutes"]!)),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        return Convert.ToBase64String(Guid.NewGuid().ToByteArray()) + DateTime.UtcNow.Ticks.ToString();
    }

    public async Task<TokenResponse> GenerateTokensAsync(UserAccount user)
    {
        var accessToken = GenerateAccessToken(user);
        var refreshToken = GenerateRefreshToken();

        var refreshTokenEntity = new RefreshToken
        {
            UserId = user.Id,
            Token = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddDays(int.Parse(configuration["Jwt:RefreshTokenExpiryDays"]!))
        };

        dbContext.RefreshTokens.Add(refreshTokenEntity);
        await dbContext.SaveChangesAsync();

        var token = new TokenResponse(accessToken, refreshToken);
        return token;
    }
}