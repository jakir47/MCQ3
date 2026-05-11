namespace MCQ3.DataConnect.Responses;

public record TokenResponse(
    string AccessToken,
    string RefreshToken
);