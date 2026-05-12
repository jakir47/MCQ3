namespace MCQ3.DataConnect.Responses;

public record FileValidationResult(bool IsValid, string? ErrorMessage = null);