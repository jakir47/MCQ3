namespace MCQ3.DataConnect.Responses;

public record UserResponse(
    Guid Id,
    string FullName,
    string Username,
    string Email,
    string Role,
    bool IsActive,
    bool TempPassword,
    DateTime CreatedAt
);