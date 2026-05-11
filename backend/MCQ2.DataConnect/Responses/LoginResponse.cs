namespace MCQ3.DataConnect.Responses;

public record LoginResponse(
    Guid UserId,
    string Username,
    string Email,
    string FullName,
    string Role,
    string AccessToken,
    string RefreshToken,
    bool TempPassword
);